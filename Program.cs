using System;
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

        private static void Main(string[] args)
        {
            videoMode = new VideoMode(800,800);
            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "Maze Crap", Styles.None);
            window.ShowMouseCursor(false);
            window.Closed += (sender, e) => ((RenderWindow) sender).Close();
            window.KeyPressed +=
                delegate(object sender, KeyEventArgs e) { if (e.Code == KeyCode.Escape) ((RenderWindow) sender).Close(); };
            SetUpMaze();
            target = new Image(VideoMode.DesktopMode.Width/10,VideoMode.DesktopMode.Height/10);
            float accumulator = 0;
            float WaitTime = 0;
            float fps = .01f;
            while (GenerateIterate(CellStack, Cells, Walls)) ;
            
            CellStack.Clear();
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
                        }
                    }
                    if (Solving && !FinishedSolving)
                    {
                        FinishedSolving =  !SolveIterate(CellStack, Cells, Walls, StartCell, EndCell);
                        accumulator -= fps;
                        continue;
                    }
                        FinishedGenerating = !GenerateIterate(CellStack, Cells, Walls);
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
            target = new Image(r.Width/10, r.Height/10);
            target.Smooth = false;
            for (int i = 0; i < Walls.GetLength(0); i++)
                for (int j = 0; j < Walls.GetLength(1); j++)
                    if (!Walls[i,j] && !VisitedWalls[i, j]) //&& (!Solving || !CellStack.Contains(new IntPair(i, j))))
                        //r.Draw(Shape.Rectangle(new FloatRect((i + 1)*10, (j + 1)*10, 10, 10), Color.White));
                        target.SetPixel((uint)i + 1, (uint)j + 1, Color.White);
            /*for (int i = 0; i < Cells.GetLength(0); i++)
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    r.Draw(Shape.Rectangle(new FloatRect((i*2 + 1)*10, (j*2 + 1)*10, 10, 10),
                                           Cells[i, j] ? Color.White : Color.Black));
                }*/
            if (!Solving)
                //r.Draw(Shape.Rectangle(new FloatRect((CurrentCell.X*2 + 1)*10, (CurrentCell.Y*2 + 1)*10, 10, 10),
                //Color.Red));
                target.SetPixel((uint)CurrentCell.X+1, (uint)CurrentCell.Y+1, Color.Red);
            if (FinishedGenerating)
                //r.Draw(Shape.Rectangle(new FloatRect((EndCell.X*2 + 1)*10, (EndCell.Y*2 + 1)*10, 10, 10), Color.Green));
                target.SetPixel((uint) EndCell.X*2+1, (uint) EndCell.Y*2+1, Color.Green);
            if (Solving)
            {
                for (int i = 0; i < VisitedWalls.GetLength(0); i++)
                    for (int j = 0; j < VisitedWalls.GetLength(1); j++)
                        if (VisitedWalls[i, j])
                            //r.Draw(Shape.Rectangle(new FloatRect((i + 1)*10, (j + 1)*10, 10, 10),
                                                   //new Color(100, 100, 100)));
                            target.SetPixel((uint) i + 1, (uint) j + 1, new Color(100, 100, 100));
                foreach (IntPair i in CellStack)
                    //r.Draw(Shape.Rectangle(new FloatRect((i.X + 1)*10, (i.Y + 1)*10, 10, 10), Color.Green));
                    target.SetPixel((uint) i.X + 1, (uint) i.Y + 1, Color.Green);
                //r.Draw(Shape.Rectangle(new FloatRect((CurrentCell.X + 1) * 10, (CurrentCell.Y + 1) * 10, 10, 10),
                                       //Color.Red));
                target.SetPixel((uint) CurrentCell.X + 1, (uint) CurrentCell.Y + 1, Color.Red);
            }
            //r.Draw(Shape.Rectangle(new FloatRect((StartCell.X*2 + 1)*10, (StartCell.Y*2 + 1)*10, 10, 10), Color.Blue));
            target.SetPixel((uint) StartCell.X*2 + 1, (uint) StartCell.Y*2 + 1, Color.Blue);
            var celltext = new Text((1/((RenderWindow)r).GetFrameTime()).ToString());//CurrentCell.ToString() + " " + EndCell.ToString() + " " + StartCell.ToString());
            celltext.Position = new Vector2(0, r.Height - celltext.GetRect().Height);
            celltext.Color = Color.Black;
            r.Draw(new Sprite {Image = target, Scale = new Vector2(10, 10)});
            r.Draw(Shape.Rectangle(celltext.GetRect(), Color.White));
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
            IntPair temp = neighbor*2 + new IntPair(0, 1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                ret = ret && temp == start || Walls[temp.X, temp.Y];
            temp = neighbor + new IntPair(1, 0);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || Walls[temp.X, temp.Y]);
            temp = neighbor*2 + new IntPair(-1, 0);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || Walls[temp.X, temp.Y]);
            temp = neighbor*2 + new IntPair(0, -1);
            if (temp.X < Maze.GetLength(0) && temp.Y < Maze.GetLength(1) && temp.X > 0 && temp.Y > 0)
                ret = ret && (temp == start || Walls[temp.X, temp.Y]);
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