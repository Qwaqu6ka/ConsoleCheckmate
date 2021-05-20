using System;
using System.Collections.Generic;
using System.Text;

namespace Checkmate {
    class Program {
        static char[,] desk = new char[10, 10];
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

            bool turn = true;
            ShowDesk(turn, ref white, ref black);
            Console.WriteLine("Turn format: |a b|, where a - start position, b - final position.");
            Console.WriteLine("The position contains a lowercase letter and a digit without a space.");
            Console.WriteLine("You need to put a space between the positions.");
            Console.WriteLine("Example: e2 e4");
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
                    Console.WriteLine("Invalid input. String must  be: \"ln(space)ln\", where l - letter, n - number");
                    continue;
                }              
                
                bool check = false;
                foreach (var p in li) {
                    if (p.let == StLet - 'a' && p.num == StNum - '1') {
                        check = true;
                        if (p.Move(turn, ref desk, ref white, ref black, FiLet - 'a', FiNum - '1')) {
                            ShowDesk(turn, ref white, ref black);
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
        public static void ShowDesk(bool turn, ref List<Piece> white, ref List<Piece> black) {
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
            if (turn) {
                for (int i = 9; i >= 0; --i) {
                    for (int j = 0; j <= 9; ++j) {
                        Console.Write(desk[j, i]);
                        Console.Write(' ');
                    }
                    Console.Write('\n');
                }
            }
            else {
                for (int i = 9; i >= 0; --i) {
                    for (int j = 9; j >= 0; --j) {
                        Console.Write(desk[j, i]);
                        Console.Write(' ');
                    }
                    Console.Write('\n');
                }
            }
        }
    }
    interface IMovable {
        bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num);
    }
    class pawn : IMovable {
        bool FirstTurn = true;
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int hor = Math.Abs(let - StLet);
            int ver = Math.Abs(num - StNum);
            if (turn) {
                if (hor == 0) {
                    if (num - StNum == 1 && (desk[let, num] == '□' || desk[let, num] == '■')) {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        return true;
                    }
                    if (num - StNum == 2 && FirstTurn && (desk[let, num] == '□' || desk[let, num] == '■')) {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        return true;
                    }
                }
                else if (hor == 1) {
                    if (num - StNum == 1 && desk[let, num] > '♚' && desk[let, num] <= '♟') {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        foreach (var piece in black) {
                            if (piece.let == let && piece.num == num) {
                                black.Remove(piece);
                                break;
                            }
                        }
                        return true;
                    }
                }
                return false;
            }
            else {
                if (hor == 0) {
                    if (num - StNum == -1 && (desk[let, num] == '□' || desk[let, num] == '■')) {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        return true;
                    }
                    if (num - StNum == -2 && FirstTurn && (desk[let, num] == '□' || desk[let, num] == '■')) {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        return true;
                    }
                }
                else if (hor == 1) {
                    if (num - StNum == -1 && desk[let, num] > '♔' && desk[let, num] <= '♙') {
                        p.let = let;
                        p.num = num;
                        FirstTurn = false;
                        foreach (var piece in white) {
                            if (piece.let == let && piece.num == num) {
                                white.Remove(piece);
                                break;
                            }
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }
    class king : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            Console.WriteLine("Перемещение короля");
            return false;
        }
    }
    class queen : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            Console.WriteLine("Перемещение ферзя");
            return false;
        }
    }
    class bishop : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            Console.WriteLine("Перемещение слона");
            return false;
        }
    }
    class knight : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            Console.WriteLine("Перемещение коня");
            return false;
        }
    }
    class rook : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            Console.WriteLine("Перемещение ладьи");
            return false;
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
        public bool Move(bool turn, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int l, int n) {
            return Movable.Move(turn, this, ref desk, ref white, ref black, let, num, l, n);
        }
    }
}