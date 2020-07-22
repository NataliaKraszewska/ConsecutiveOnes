using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConOnesProject
{
    class MoveInTabuList
    {
        public int randomColumnIndex;
        public int randomPosition;
        public int column;

        public MoveInTabuList(int in_randomColumnIndex, int in_randomPosition, int in_column)
        {
            randomColumnIndex = in_randomColumnIndex;
            randomPosition = in_randomPosition;
            column = in_column;
        }
    }
}