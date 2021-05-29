using System;
using System.Collections.Generic;

namespace Figure {
	interface IMovable {
        bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num);
    }
    class king : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int ver = Math.Abs(num - StNum);
            int hor = Math.Abs(let - StLet);
            if ((ver == 1 && hor < 2) || (hor == 1 && ver < 2))
                return true;
            return false;
        }
    }
    class knight : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int ver = Math.Abs(num - StNum);
            int hor = Math.Abs(let - StLet);
            if ((ver == 2 && hor == 1) || (ver == 1 && hor == 2))
                return true;
            return false;
        }
    }
    /* слон ферзь и лядья в своих классах проверяют поля между стартовой и конечной клеткой на пустоту */
    class bishop : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int ver = Math.Abs(num - StNum);
            int hor = Math.Abs(let - StLet);
            if (ver == hor && ver != 0) {
                // check space in the middle
                bool l = false;
                if (let - StLet > 0) l = true;  // if l == true, then it turn to the right side
                bool n = false;
                if (num - StNum > 0) n = true;  // if n == true, then it turn to the up side
        
                for (int i = 1; i < hor; ++i) {
                    int LetIter = i, NumIter = i;
                    if (!l) LetIter *= -1;
                    if (!n) NumIter *= -1;
                    if (!(desk[StLet + LetIter, StNum + NumIter] == '□' || desk[StLet + LetIter, StNum + NumIter] == '■'))
                        return false;
                }
                return true;
            }
            return false;
        }
    }
    class rook : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int ver = Math.Abs(num - StNum);
            int hor = Math.Abs(let - StLet);
            if ((ver == 0 && hor != 0) || (hor == 0 && ver != 0)) {
                // check space in the middle
                bool PositivDir = true;
                if (num - StNum < 0 || let - StLet < 0)
                    PositivDir = false;
                if (ver == 0) {
                    for (int i = 1; i < hor; ++i) {
                        int iter = i;
                        if (!PositivDir) iter *= -1;
                        if (!(desk[StLet + iter, StNum] == '□' || desk[StLet + iter, StNum] == '■'))
                            return false;
                    }
                } 
                if (hor == 0) {
                    for (int i = 1; i < ver; ++i) {
                        int iter = i;
                        if (!PositivDir) iter *= -1;
                        if (!(desk[StLet, StNum + iter] == '□' || desk[StLet, StNum + iter] == '■'))
                            return false;
                    }
                } 
                return true;
            }
            return false;
        }
    }
    class queen : IMovable {
        public bool Move(bool turn, Piece p, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int StLet, int StNum, int let, int num) {
            int ver = Math.Abs(num - StNum);
            int hor = Math.Abs(let - StLet);
            if ((ver == hor && ver != 0) || (ver == 0 && hor != 0) || (hor == 0 && ver != 0)) {
                if (ver == hor) {
                    bool l = false;
                    if (let - StLet > 0) l = true;  // if l == true, then it turn to the right side
                    bool n = false;
                    if (num - StNum > 0) n = true;  // if n == true, then it turn to the up side
            
                    for (int i = 1; i < hor; ++i) {
                        int LetIter = i, NumIter = i;
                        if (!l) LetIter *= -1;
                        if (!n) NumIter *= -1;
                        if (!(desk[StLet + LetIter, StNum + NumIter] == '□' || desk[StLet + LetIter, StNum + NumIter] == '■'))
                            return false;
                    }
                    return true;
                }

                bool PositivDir = true;
                if (num - StNum < 0 || let - StLet < 0)
                    PositivDir = false;
                if (ver == 0) {
                    for (int i = 1; i < hor; ++i) {
                        int iter = i;
                        if (!PositivDir) iter *= -1;
                        if (!(desk[StLet + iter, StNum] == '□' || desk[StLet + iter, StNum] == '■'))
                            return false;
                    }
                } 
                else if (hor == 0) {
                    for (int i = 1; i < ver; ++i) {
                        int iter = i;
                        if (!PositivDir) iter *= -1;
                        if (!(desk[StLet, StNum + iter] == '□' || desk[StLet, StNum + iter] == '■'))
                            return false;
                    }
                }
                return true;
            }
            return false;
        }
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
                    if (num - StNum == 1 && desk[let, num] > '♔' && desk[let, num] <= '♙') {
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
                    if (num - StNum == -1 && desk[let, num] > '♚' && desk[let, num] <= '♟') {
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
    class Piece {
        public char pic;
        public int let;
        public int num;
        public bool isPawn;
        public Piece(char pic, int let, int num, IMovable mov, bool Pawnivision = false) {
            this.pic = pic;
            this.let = let;
            this.num = num;
            isPawn = Pawnivision;
            Movable = mov;
        }
        public IMovable Movable { get; set; }
        /* У пешки ход особенный. Для остальных фигур проверяю свободна ли конечная клетка или же там противник*/
        public bool Move(bool turn, ref char[,] desk, ref List<Piece> white, ref List<Piece> black, int l, int n) {
            if (isPawn) 
                return Movable.Move(turn, this, ref desk, ref white, ref black, let, num, l, n);
            else {
                // follow method checks validity of turn
                bool accept = Movable.Move(turn, this, ref desk, ref white, ref black, let, num, l, n);
                if (accept) {
                    if (desk[l, n] == '□' || desk[l, n] == '■') {
                        let = l;
                        num = n;
                        return true;
                    }
                    if (turn) {
                        if (desk[l, n] > '♔' && desk[l, n] <= '♙') {
                            let = l;
                            num = n;
                            foreach(Piece piece in black) {
                                if (piece.let == l && piece.num == n) {
                                    black.Remove(piece);
                                    break;
                                }
                            }
                            return true;
                        }
                    }
                    else {
                        if (desk[l, n] > '♚' && desk[l, n] <= '♟') {
                            let = l;
                            num = n;
                            foreach(Piece piece in white) {
                                if (piece.let == l && piece.num == n) {
                                    white.Remove(piece);
                                    break;
                                }
                            }
                            return true;
                        }
                    }
                    return false;
                }
                return false;
            }
        }
    }
}