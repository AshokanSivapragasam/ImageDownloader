/// From the sample diagram in problem, I understood to organize the list of squares diagonally in the increasing order
/// For documentation, I used native XML documentation comments for better presentation and understanding.
/// 
/// REQUIREMENTS:
///     Requirement #1: To sort the side of squares in the increasing order
///     Requirement #2: To place successive square blocks to exactly right top corner of each square
///   
/// This program will get the sample inputs (4,5,4,6,10) and generates output in a graph from (0,0) to (19,19).

using System;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Class with auxiliary methods to sort the squares diagonally in increasing order.
/// </summary>
class SquareSort
{
    /// <summary>
    /// Main driver
    /// </summary>
    public static void Main1()
    {
        #region PREPARE_LIST_OF_UNSORTED_SQUARES
        // Instantiate the list of unsorted squares
        // This list will persist the user inputs
        List<Square> unsortedSquares = new List<Square>
        {
            new Square(6)
            ,new Square(5)
            ,new Square(4)
            ,new Square(10)
            ,new Square(4)
        };
        #endregion

        #region DISPLAY_LIST_OF_UNSORTED_SQUARES
        Console.WriteLine("Unsorted Square List".PadLeft(40, '*').PadRight(60, '*'));
        foreach (Square unsortedSquare in unsortedSquares)
        {
            // Print the results
            Console.WriteLine("Square | Side: {0} | Bottom left corner (X, Y): ({1}, {2})", unsortedSquare.GetSide(), unsortedSquare.GetXPosition(), unsortedSquare.GetYPosition());
        }
        #endregion

        #region SORT_LIST_OF_SQUARES_DIAGONALLY_IN_INCREASING_ORDER
        var sortedSquares = new List<Square>(Square.Diagonalize(unsortedSquares));
        #endregion

        #region DISPLAY_LIST_OF_SORTED_SQUARES
        Console.WriteLine(Environment.NewLine + Environment.NewLine + "Sorted Square List".PadLeft(40, '*').PadRight(60, '*'));
        foreach (Square sortedSquare in sortedSquares) // An iterator object is created to iterate the SortedSquare to print the results
        {
            // Print the results
            Console.WriteLine("Square | Side: {0} | Bottom left corner (X, Y): ({1}, {2})", sortedSquare.GetSide(), sortedSquare.GetXPosition(), sortedSquare.GetYPosition());
        }
        #endregion
    }
}

/// <summary>
/// this class is used to define Square as a datatype to the list and helps to sort the Square in the incresing order  
/// </summary>
internal class Square // version of 15-09-2015
{
    // m_side variable used to assign side values(Square has equally four sides. 
    private double m_Side;
    //m_PosX is used to mark square from left to right.
    private double m_PosX = 0;
    //m_Posx is used to mark square from bottom to top.
    private double m_PosY = 0;

    /// <summary>
    /// Constructor called to initiatize new object for class, 'Square' with side value
    /// </summary>
    /// <param name="p_S"></param>
    public Square(double p_S)
    {
        m_Side = p_S;
    }

    /// <summary>
    /// SetPosition methoed is used to assign (X,Y) positions 
    /// </summary>
    /// <param name="p_PX">X_position </param>
    /// <param name="p_PY">Y_position</param>
    public void SetPosition(double p_PX, double p_PY)
    {
        m_PosX = p_PX;
        m_PosY = p_PY;
    }

    /// <summary>
    /// It returns side of a square.
    /// </summary>
    /// <returns>Square side value</returns>
    public double GetSide()
    {
        return m_Side;
    }

    /// <summary>
    /// It return 'x' position of a square
    /// </summary>
    /// <returns>XPosition</returns>
    public double GetXPosition()
    {
        return m_PosX;
    }

    /// <summary>
    /// It return 'y' position of a square
    /// </summary>
    /// <returns>Y_position</returns>
    public double GetYPosition()
    {
        return m_PosY;
    }

    /// <summary>
    /// This method is used to organize the list of squares diagonally in the increasing order
    /// 
    /// REQUIREMENTS:
    ///     Requirement #1: To sort the side of squares in the increasing order
    ///     Requirement #2: To place successive square blocks to exactly right top corner of each square
    ///     
    /// </summary>
    /// <param name="p_Squares">List of Square objects</param>
    /// <returns>Sorted Square objects</returns>
    public static List<Square> Diagonalize(List<Square> p_Squares)
    {
        // It holds (x, y) coordinates of successive square in the list
        double positionXForNextSquare = 0, positionYForNextSquare = 0;

        /*
         * Requirement #1: To sort the side of squares in the increasing order
         * 
         * Passing 'p_Squares' as reference to method, 'SortListOfSquares' so that any change inside the method will persist in caller method, 'Diagnolize()'
         * Note: We can also acheive by 'pass by value'.
         */
        SortListOfSquares(ref p_Squares);

        foreach (var p_Square in p_Squares)
        {
            /*
             * Requirement #2: To place successive square blocks to exactly right top corner of each square
             * 
             * 
             * Initially the values for (positionXForNextSquare, positionYForNextSquare) is (0, 0)
             * From next iteration, the values are being manipulated as (positionXForNextSquare + p_Side, positionYForNextSquare + p_Side).
             * So, the values will be,
             *      Consider sides as 4, 4, 5, 6
             *      Pass #0: (0, 0)
             *      Pass #0: (4, 4)
             *      Pass #0: (8, 8)
             *      Pass #0: (13, 13)
             *      Pass #0: (19, 19)
             */
            p_Square.SetPosition(positionXForNextSquare, positionYForNextSquare);
            positionXForNextSquare += p_Square.GetSide();
            positionYForNextSquare += p_Square.GetSide();
        }

        // Returns list, 'p_squares' in the expected order and arrangements
        return p_Squares;
    }

    /// <summary>
    /// This methoed uses Bubble Sort algorithm to sort Square values in the increasing order.
    /// </summary>
    /// <param name="p_Squares">List of square objects</param>
    public static void SortListOfSquares(ref List<Square> p_Squares)
    {
        // Iterate from first element through last element in the list
        for (int idx = 0; idx < p_Squares.Count; idx++)
        {
            // Iterate from element right next to 1st iterator, 'idx' until end of the list.
            for (int idy = idx + 1; idy < p_Squares.Count; idy++)
            {
                // True - if element#1 is greater than the element #2; element with larger value bubbles up to far end
                // False - if element#1 is lesser than the element #2; no action
                if (p_Squares[idx].GetSide() > p_Squares[idy].GetSide())
                {
                    /// Swapping two square objects and iterates the next value
                    var temp_Square = p_Squares[idx];
                    p_Squares[idx] = p_Squares[idy];
                    p_Squares[idy] = temp_Square;
                }
            }
        }
    }
}
