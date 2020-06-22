using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class ConsString
    {
        int startInRow;
        int endInRow;
        List<int> seq;
        public int id;

        public ConsString(int inputId, int inputStart, int inputEnd, List<int> inputSeq)
        {
            startInRow = inputStart;
            endInRow = inputEnd;
            id = inputId;
            seq = inputSeq;
        }


        public int getStart(int inputId)
        {
            if (inputId == id)
                return startInRow;
            return 0;
        }


        public int getEnd(int inputId)
        {
            if (inputId == id)
                return endInRow;
            return 0;
        }


        public List<int> getSeq(int inputId)
        {
            if (inputId == id)
                return seq;
            return null;
        }

    }
}
