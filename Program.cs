using System;
using System.Collections.Generic;
using System.Text;

namespace Checkmate {
    class Program {
        static void Main(string[] args) {
            Console.OutputEncoding = Encoding.Unicode;  // for print checkmate pieces
            List<Piece> white = new List<Piece>();  // container for white pieces
            white.Add(new Piece('\u265C', 1, 1, new rook()));
            white.Add(new Piece('\u265E', 2, 1, new knight()));
            white.Add(new Piece('\u265D', 3, 1, new bishop()));
            white.Add(new Piece('\u265B', 4, 1, new queen()));
            white.Add(new Piece('\u265A', 5, 1, new king()));
            white.Add(new Piece('\u265D', 6, 1, new bishop()));
            white.Add(new Piece('\u265E', 7, 1, new knight()));
            white.Add(new Piece('\u265C', 8, 1, new rook()));
            for (int i = 1; i <= 8; ++i) {
                white.Add(new Piece('\u265F', i, 2, new pawn()));
            }
            List<Piece> black = new List<Piece>();  // container for black pieces
            black.Add(new Piece('\u2656', 1, 8, new rook()));
            black.Add(new Piece('\u2658', 2, 8, new knight()));
            black.Add(new Piece('\u2657', 3, 8, new bishop()));
            black.Add(new Piece('\u2655', 4, 8, new queen()));
            black.Add(new Piece('\u2654', 5, 8, new king()));
            black.Add(new Piece('\u2657', 6, 8, new bishop()));
            black.Add(new Piece('\u2658', 7, 8, new knight()));
            black.Add(new Piece('\u2656', 8, 8, new rook()));
            for (int i = 1; i <= 8; ++i) {
                black.Add(new Piece('\u2659', i, 7, new pawn()));
            }
            ShowDesk(ref white, ref black);
            Console.WriteLine("Turn format: |a b|, where a - start position, b - final position.\n",
            "The position contains a lowercase letter and a digit without a space.\n",
            "You need to put a space between the positions.\n", "Example: e2 e4");
            bool turn = true;
            ref List<Piece> li = ref white;
            while (true) {
                if (turn) {
                    li = ref white;
                    Console.Write("White's turn: ");
                }
                else {
                    li = ref black;
                    Console.Write("Black's turn: ");
                }

                string str = Console.ReadLine();
                if (str.Length != 5) {
                    Console.WriteLine("Invalid input. String have to be: \"ld(space)ld\", where l - letter, d - digit");
                    continue;
                }

                char StLet = str[0], StNum = str[1];
                char FiLet = str[3], FiNum = str[4];

                if (StLet < 'a' || StLet > 'h' || FiLet < 'a' || FiLet > 'h' ||
                StNum < '1' || StNum > '8' || FiNum < '1' || FiNum > '8') {
                    Console.WriteLine("Invalid input. String must be: \"ln(space)ln\", where l - letter, n - number");
                    continue;
                }              
                
                bool check = false;
                foreach (var p in li) {
                    if (p.let == StLet && p.num == StNum) {
                        check = true;
                        if (p.Move(FiLet, FiNum)) {
                            ShowDesk(ref white, ref black);
                            turn = !turn;
                            break;
                        } 
                        else {
                            Console.WriteLine("Wrong move");
                            break;
                        }
                    }
                }
                if (!check)
                    Console.WriteLine("There is no piece at the starting position");
            }
        }

        // function prints desk with current pieces location
        public static void ShowDesk(ref List<Piece> white, ref List<Piece> black) {
            char[,] desk = new char[10, 10];
            string a = "abcdefgh";
            string b = "12345678";
            for (int i = 1; i <= 8; ++i) {  // fill edges letters and nubbers
                desk[0, i] = b[i - 1];
                desk[9, i] = b[i - 1];
                desk[i, 0] = a[i - 1];
                desk[i, 9] = a[i - 1];
            }
            for (int i = 1; i <= 7; i += 2) {   // fill desk with black cells
                for (int j = 1; j < 7; j += 2) {
                    desk[i, j] = '□';
                }
            }
            for (int i = 2; i <= 8; i += 2) {
                for (int j = 2; j < 8; j += 2) {
                    desk[i, j] = '□';
                }
            } 
            for (int i = 1; i <= 7; i += 2) {   // fill desk with white cells
                for (int j = 2; j < 8; j += 2) {
                    desk[i, j] = '■';
                }
            }
            for (int i = 2; i <= 8; i += 2) {
                for (int j = 1; j < 7; j += 2) {
                    desk[i, j] = '■';
                }
            } 
            foreach (Piece p in white) {    // fill desk with pieces
                desk[p.let, p.num] = p.pic;
            }
            foreach (Piece p in black) {
                desk[p.let, p.num] = p.pic;
            }
            for (int i = 9; i >= 0; --i) {
                for (int j = 0; j <= 9; ++j) {
                    Console.Write(desk[j, i]);
                    Console.Write(' ');
                }
                Console.Write('\n');
            }
        }
    }
    interface IMovable {
        bool Move();
    }
    class pawn : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на бензине");
        }
    }
    class king : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на бензине");
        }
    }
    class queen : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на бензине");
        }
    }
    class bishop : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на бензине");
        }
    }
    class knight : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на электричестве");
        }
    }
    class rook : IMovable {
        public bool Move() {
            Console.WriteLine("Перемещение на электричестве");
        }
    }
    class Piece {
        public char pic;
        public int let;
        public int num;
        public Piece(char pic, int let, int num, IMovable mov) {
            this.pic = pic;
            this.let = let;
            this.num = num;
            Movable = mov;
        }
        public IMovable Movable { get; set; }
        public bool Move() {
            return Movable.Move();
        }
    }
}