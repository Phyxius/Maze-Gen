﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace MazeGenSolve
{
    internal class Program
    {
        private static bool[,] Cells, Walls, VisitedWalls;
        private static bool started = false;
        private static bool FinishedGenerating = false;
        private static bool FinishedSolving = false;
        private static bool Solving;
        private static Stack<IntPair> CellStack;
        private static Random r = new Random();
        private static IntPair CurrentCell, StartCell, EndCell;
        private static VideoMode videoMode;
        private static int BackTrackLevel;
        private static int iter = 0;
        private static bool NeedsRedraw;
        private static Image target;

        private static Color CurrentCellColor = ConvertColor(Properties.Settings.Default.CurrentCellColor),
                             ForegroundColor = ConvertColor(Properties.Settings.Default.ForegroundColor),
                             VisitedCellColor = ConvertColor(Properties.Settings.Default.VisitedCellColor),
                             EndCellColor = ConvertColor(Properties.Settings.Default.EndCellColor),
                             BeginCellColor = ConvertColor(Properties.Settings.Default.BeginCellColor),
                             BackgroundColor = ConvertColor(Properties.Settings.Default.BackgroundColor),
                             PathColor = ConvertColor(Properties.Settings.Default.PathColor);

        private static void Main(string[] args)
        {
            videoMode = VideoMode.DesktopMode;
            RenderWindow window = new RenderWindow(videoMode, "Maze Crap", Styles.Titlebar);
            window.ShowMouseCursor(false);
            window.Closed += (sender, e) => System.Windows.Forms.Application.Exit(); //((RenderWindow) sender).Close();
            window.KeyPressed += (sender, e) => ((RenderWindow) sender).Close();
            SetUpMaze();
            target = new Image(VideoMode.DesktopMode.Width/10, VideoMode.DesktopMode.Height/10) { Smooth = false};
            float accumulator = 0;
            float WaitTime = 0;
            float fps = (float)Properties.Settings.Default.Framerate;
            //while (GenerateIterate(CellStack, Cells, Walls)) ;
            //FinishedGenerating = true;
            //Solving = true;
            //CellStack.Clear();
            //while (SolveIterate(CellStack, Cells, Walls, StartCell, EndCell)) ;
            window.Show(true);
            while (window.IsOpened())
            {
                accumulator += window.GetFrameTime();
                while (accumulator > fps)
                {

                    if (FinishedGenerating && !Solving)
                    {
                        if (WaitTime < 2)
                            WaitTime += window.GetFrameTime();
                        else
                        {
                            //SetUpMaze();
                            //FinishedGenerating = false;
                            WaitTime = 0;
                            CurrentCell = StartCell*2;
                            VisitedWalls[CurrentCell.X, CurrentCell.Y] = true;
                            CellStack.Clear();
                            Solving = true;
                        }
                        accumulator -= fps;
                        continue;
                    }
                    if (Solving && FinishedSolving)
                    {
                        if (WaitTime < 2)
                            WaitTime += window.GetFrameTime();
                        else
                        {
                            Solving = false;
                            FinishedGenerating = false;
                            FinishedSolving = false;
                            SetUpMaze();
                            continue;
                        }
                        continue;
                    }
                    if (Solving && !FinishedSolving)
                    {
                        if (Properties.Settings.Default.ShowSolving)
                            FinishedSolving = !SolveIterate(CellStack, Cells, Walls, StartCell, EndCell);
                        else
                        {
                            while (SolveIterate(CellStack, Cells, Walls, StartCell, EndCell)) ;
                            FinishedSolving = true;
                        }
                        accumulator -= fps;
                        continue;
                    }

                    if (Properties.Settings.Default.ShowGeneration)
                        FinishedGenerating = !GenerateIterate(CellStack, Cells, Walls);
                    else
                    {
                        while (GenerateIterate(CellStack, Cells, Walls)) ;
                        FinishedGenerating = true;
                    }
                    accumulator -= fps;
                    NeedsRedraw = true;
                }
                Render(window);
                window.Display();
                window.DispatchEvents();
            }
        }

        private static bool SolveIterate(Stack<IntPair> cellStack, bool[,] cells, bool[,] walls, IntPair StartCell,
                                         IntPair Endcell)
        {
            iter++;
            if (CurrentCell == Endcell*2)
                return false;
            IntPair pos = CurrentCell;
            List<IntPair> neighbors = new List<IntPair>();
            var tempcell = new IntPair(CurrentCell.X + 1, CurrentCell.Y);
            if(tempcell.X < walls.GetLength(0) && !walls[tempcell.X, tempcell.Y] && !VisitedWalls[tempcell.X, tempcell.Y])
                neighbors.Add(tempcell);
            tempcell = new IntPair(CurrentCell.X, CurrentCell.Y + 1);
            if (tempcell.Y < walls.GetLength(1) && !walls[tempcell.X, tempcell.Y] && !VisitedWalls[tempcell.X, tempcell.Y])
                neighbors.Add(tempcell);
            tempcell = new IntPair(CurrentCell.X, CurrentCell.Y - 1);
            if (tempcell.Y >=0 && !walls[tempcell.X, tempcell.Y] && !VisitedWalls[tempcell.X, tempcell.Y])
                neighbors.Add(tempcell);
            tempcell = new IntPair(CurrentCell.X - 1, CurrentCell.Y );
            if (tempcell.X >= 0 && !walls[tempcell.X, tempcell.Y] && !VisitedWalls[tempcell.X, tempcell.Y])
                neighbors.Add(tempcell);

            if (neighbors.Count == 0)
            {
                if (CellStack.Count == 0)
                    return false;
                CurrentCell = CellStack.Pop();
                return true;
            }
            if (cellStack.Count > 0 && cellStack.Peek()!=CurrentCell)
                cellStack.Push(CurrentCell);
            IntPair newpos = neighbors[r.Next(neighbors.Count)];
            VisitedWalls[newpos.X, newpos.Y] = true;
            //VisitedWalls[(newpos.X*2 + CurrentCell.X*2)/2, (newpos.Y*2 + CurrentCell.Y*2)/2] = true;
            cellStack.Push(newpos);
            CurrentCell = newpos;
            return true;
        }

        private static void SetUpMaze()
        {
            Cells = new bool[videoMode.Width/20 - 1,videoMode.Height/20 - 1];
            Walls = new bool[videoMode.Width/10 - 2,videoMode.Height/10 - 2];
            VisitedWalls = new bool[videoMode.Width/10 - 2,videoMode.Height/10 - 2];
            CellStack = new Stack<IntPair>();
            FinishedGenerating = false;
            Solving = false;
            CurrentCell = new IntPair();
            /*for (int i = 0; i < Cells.GetLength(0); i++)
                for (int j = 0; j < Cells.GetLength(1); j++)
                    Cells[i, j] = (i%2 == 1);*/
            for (int i = 0; i < Walls.GetLength(0); i++)
                for (int j = 0; j < Walls.GetLength(1); j++)
                {
                    Walls[i, j] = true;
                }
        }

        private static void Render(RenderTarget r)
        {
            if (!NeedsRedraw)
            {
                return;
            }
            //r.Draw(Shape.Rectangle(new FloatRect(0, 0, r.Width, r.Height), Color.Black));
            /*for (uint i = 0; i < target.Width; i++)
                for (uint j = 0; j < target.Height; j++)
                    target.SetPixel(i+1, j+1, BackgroundColor);*/
            for (int i = 0; i < Walls.GetLength(0); i++)
                for (int j = 0; j < Walls.GetLength(1); j++)
                    if (!Walls[i,j] && !VisitedWalls[i, j]) //&& (!Solving || !CellStack.Contains(new IntPair(i, j))))
                        //r.Draw(Shape.Rectangle(new FloatRect((i + 1)*10, (j + 1)*10, 10, 10), Color.White));
                        target.SetPixel((uint)i + 1, (uint)j + 1, ForegroundColor);
                    else
                        target.SetPixel((uint)i+1, (uint) j+1, BackgroundColor);
            /*for (int i = 0; i < Cells.GetLength(0); i++)
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    r.Draw(Shape.Rectangle(new FloatRect((i*2 + 1)*10, (j*2 + 1)*10, 10, 10),
                                           Cells[i, j] ? Color.White : Color.Black));
                }*/
            if (!Solving)
                //r.Draw(Shape.Rectangle(new FloatRect((CurrentCell.X*2 + 1)*10, (CurrentCell.Y*2 + 1)*10, 10, 10),
                //Color.Red));
                target.SetPixel((uint)CurrentCell.X*2+1, (uint)CurrentCell.Y*2+1, CurrentCellColor);
            if (FinishedGenerating)
                //r.Draw(Shape.Rectangle(new FloatRect((EndCell.X*2 + 1)*10, (EndCell.Y*2 + 1)*10, 10, 10), Color.Green));
                target.SetPixel((uint) EndCell.X*2+1, (uint) EndCell.Y*2+1, EndCellColor);
            if (Solving)
            {
                for (int i = 0; i < VisitedWalls.GetLength(0); i++)
                    for (int j = 0; j < VisitedWalls.GetLength(1); j++)
                        if (VisitedWalls[i, j])
                            //r.Draw(Shape.Rectangle(new FloatRect((i + 1)*10, (j + 1)*10, 10, 10),
                                                   //new Color(100, 100, 100)));
                            target.SetPixel((uint) i + 1, (uint) j + 1, VisitedCellColor);
                foreach (IntPair i in CellStack)
                    //r.Draw(Shape.Rectangle(new FloatRect((i.X + 1)*10, (i.Y + 1)*10, 10, 10), Color.Green));
                    target.SetPixel((uint) i.X + 1, (uint) i.Y + 1, PathColor);
                //r.Draw(Shape.Rectangle(new FloatRect((CurrentCell.X + 1) * 10, (CurrentCell.Y + 1) * 10, 10, 10),
                                       //Color.Red));
                target.SetPixel((uint) (CurrentCell.X * (Solving ? 1 : 2) + 1), (uint) CurrentCell.Y + 1, CurrentCellColor);
            }
            //r.Draw(Shape.Rectangle(new FloatRect((StartCell.X*2 + 1)*10, (StartCell.Y*2 + 1)*10, 10, 10), Color.Blue));
            target.SetPixel((uint) StartCell.X*2 + 1, (uint) StartCell.Y*2 + 1, BeginCellColor);
            var celltext = new Text((1/((RenderWindow)r).GetFrameTime()).ToString());//CurrentCell.ToString() + " " + EndCell.ToString() + " " + StartCell.ToString());
            celltext.Position = new Vector2(0, r.Height - celltext.GetRect().Height);
            celltext.Color = BackgroundColor;
            r.Draw(new Sprite {Image = target, Scale = new Vector2(10, 10)});
            r.Draw(Shape.Rectangle(celltext.GetRect(), ForegroundColor));
            r.Draw(celltext);
        }

        private static bool GenerateIterate(Stack<IntPair> CellStack, bool[,] Maze, bool[,] Walls)
        {
            if (CellStack.Count == 0 && CurrentCell == new IntPair(0, 0))
            {
                if (started)
                    return false;
                IntPair temppos = new IntPair(r.Next(Maze.GetLength(0)), (r.Next(Maze.GetLength(1))));
                Maze[temppos.X, temppos.Y] = true;
                Walls[temppos.X*2, temppos.Y*2] = false;
                CellStack.Push(temppos);
                CurrentCell = temppos;
                StartCell = temppos;
                BackTrackLevel++;
                return true;
            }
            IntPair pos = CurrentCell;
            List<IntPair> neighbors = new List<IntPair>();
            if (pos.X + 1 < Maze.GetLength(0) && !Maze[pos.X + 1, pos.Y] &&
                IntPair.CheckNeighbors(pos, new IntPair(pos.X + 1, pos.Y), Maze, Walls))
                neighbors.Add(new IntPair(pos.X + 1, pos.Y));
            if (pos.Y + 1 < Maze.GetLength(1) && !Maze[pos.X, pos.Y + 1] &&
                IntPair.CheckNeighbors(pos, new IntPair(pos.X, pos.Y + 1), Maze, Walls))
                neighbors.Add(new IntPair(pos.X, pos.Y + 1));
            if (pos.X - 1 >= 0 && !Maze[pos.X - 1, pos.Y] &&
                IntPair.CheckNeighbors(pos, new IntPair(pos.X - 1, pos.Y), Maze, Walls))
                neighbors.Add(new IntPair(pos.X - 1, pos.Y));
            if (pos.Y - 1 >= 0 && !Maze[pos.X, pos.Y - 1] &&
                IntPair.CheckNeighbors(pos, new IntPair(pos.X, pos.Y - 1), Maze, Walls))
                neighbors.Add(new IntPair(pos.X, pos.Y - 1));
            if (neighbors.Count == 0)
            {
                if (CellStack.Count == 0)
                    return false;
                CurrentCell = CellStack.Pop();
                BackTrackLevel--;
                return true;
            }
            IntPair newpos = neighbors[r.Next(neighbors.Count)];
            Maze[newpos.X, newpos.Y] = true;
            Walls[newpos.X*2, newpos.Y*2] = false;
            Walls[(newpos.X*2 + CurrentCell.X*2)/2, (newpos.Y*2 + CurrentCell.Y*2)/2] = false;
            CellStack.Push(newpos);
            CurrentCell = newpos;
            //EndCell = newpos;
            BackTrackLevel++;
            if (BackTrackLevel >= 0)
            {
                EndCell = newpos;
                BackTrackLevel = 0;
            }
            return true;
        }

        static Color ConvertColor(System.Drawing.Color c)
        {
            return new Color(c.R, c.G, c.B);
        }
    }

    internal struct IntPair
    {
        public readonly int X;
        public readonly int Y;

        public IntPair(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool CheckNeighbors(IntPair start, IntPair neighbor, bool[,] Maze, bool[,] Walls)
        {
            bool ret = true;
            try
            {
                IntPair temp = neighbor * 2 + new IntPair(0, 1);
                if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                    ret = ret && temp == start || Walls[temp.X, temp.Y];
                temp = neighbor + new IntPair(1, 0);
                if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                    ret = ret && (temp == start || Walls[temp.X, temp.Y]);
                temp = neighbor * 2 + new IntPair(-1, 0);
                if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                    ret = ret && (temp == start || Walls[temp.X, temp.Y]);
                temp = neighbor * 2 + new IntPair(0, -1);
                if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                    ret = ret && (temp == start || Walls[temp.X, temp.Y]);
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            /*temp = neighbor*2 + new IntPair(1, 1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) - 1 && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || !Maze[temp.X, temp.Y]);
            temp = neighbor*2 + new IntPair(1, -1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) - 1 && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || !Maze[temp.X, temp.Y]);
            temp = neighbor*2 + new IntPair(-1, 1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) - 1 && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || !Maze[temp.X, temp.Y]);
            temp = neighbor*2 + new IntPair(-1, -1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) - 1 && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || !Maze[temp.X, temp.Y]);*/
            return ret;
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ")";
        }

        public static bool operator ==(IntPair a, IntPair b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(IntPair a, IntPair b)
        {
            return !(a == b);
        }

        public static IntPair operator +(IntPair a, IntPair b)
        {
            return new IntPair(a.X + b.X, a.Y + b.Y);
        }

        public static IntPair operator -(IntPair a, IntPair b)
        {
            return new IntPair(a.X - b.X, a.Y - b.Y);
        }

        public static IntPair operator *(IntPair a, int b)
        {
            return new IntPair(a.X*b, a.Y*b);
        }

        public static IntPair operator /(IntPair a, int b)
        {
            return new IntPair(a.X/b, a.Y/b);
        }
    }
}