using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearchForConsOnesProblem
{
    class MovedColumnAndNeighborhood
    {
        public int column;
        public int left_neighborhood;
        public int right_neighborhood;
        public int neighborhood;

        public MovedColumnAndNeighborhood(int in_column, int in_left_neighborhood, int in_right_neighborhood)
        {
            column = in_column;
            left_neighborhood = in_left_neighborhood;
            right_neighborhood = in_right_neighborhood;
        }


        public MovedColumnAndNeighborhood(int in_column, int in_neighborhood)
        {
            column = in_column;
            neighborhood = in_neighborhood;
        }

    }
}
