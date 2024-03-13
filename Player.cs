using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Player
    {
        public string Name { get; set; }
        public Board OwnBoard { get; set; }
        public Board TargetBoard { get; set; }

        public Player(string name)
        {
            Name = name;
            OwnBoard = new Board();
            TargetBoard = new Board();
        }
    }

}
