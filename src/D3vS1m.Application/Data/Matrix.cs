using D3vS1m.Domain.System.Exceptions;
using System;

namespace D3vS1m.Application.Data
{
    /// <summary>
    /// A class to create a matrix data container with generic data type.
    /// </summary>
    /// <typeparam name="T">The concrete data type</typeparam>
    [Serializable]
    public class Matrix<T>
    {

        /*
         * generic jagged array:
         * not a 2D-Array, because they are slower. In some cases. In some cases they need inner loops and jagged arrays don't.
         * Jagged arreay also allow triangular matrices, because there are no depenencies between the rows.
         */
        protected T[][] _matrix;

        // --- constructors

        #region constructor (2)
        public Matrix()
        {

        }

        public Matrix(int rows, int cols)
        {
            Init(rows, cols);
        }
        #endregion

        // --- methods

        /// <summary>
        /// (Re-)Initializes the matrix and sets all elements with default()T values of the defined type.
        /// Existent elements will be removed.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        public void Init(int rows, int cols)
        {
            Init(rows, cols, default);
        }

        /// <summary>
        /// (Re-)Initializes the matrix and sets all elements to the value of the defined type.
        /// Existent elements will be removed.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of columns.</param>
        /// <param name="value">The initial value for each element.</param>
        public void Init(int rows, int cols, T value)
        {
            RowsCount = rows;
            ColsCount = cols;

            _matrix = new T[RowsCount][];
            for (int r = 0; r < RowsCount; r++)
            {
                _matrix[r] = new T[ColsCount];
                for (int c = 0; c < ColsCount; c++)
                {
                    _matrix[r][c] = value;
                }
            }
        }

        /// <summary>
        /// Runs a function on each element in the matrix.
        /// </summary>
        /// <param name="function">The delegate funktion needs the parameters  row:int, column:int, the value T and
        /// returns the the manipulated value of T of the matrix.</param>
        public virtual void Each(Func<int, int, T, T> function)
        {
            for (int r = 0; r < RowsCount; r++)
            {
                for (int c = 0; c < ColsCount; c++)
                {
                    _matrix[r][c] = function(r, c, _matrix[r][c]);
                }
            }
        }

        /// <summary>
        /// Gets a single element from the matrix after a validation of the input parameters.
        /// </summary>
        /// <param name="rows">The index of the row.</param>
        /// <param name="cols">The index of the column.</param>
        /// <returns>The value of the matrix.</returns>
        public T Get(int row, int col)
        {
            validate(row, col);
            return _matrix[row][col];
        }

        /// <summary>
        /// Gets a complete row from the matrix after a validation of the input parameters.
        /// </summary>
        /// <param name="row">The index of the row.</param>
        /// <returns>The value array of the matrix.</returns>
        public T[] GetRow(int row)
        {
            validateRow(row);
            return _matrix[row];
        }

        /// <summary>
        /// Gets a complete column from the matrix after a validation of the input parameters.
        /// </summary>
        /// <param name="col">The index of the column.</param>
        /// <returns>The value array of the matrix.</returns>
        public T[] GetCol(int col)
        {
            validateCol(col);

            T[] colArray = new T[RowsCount];
            for (int r = 0; r < RowsCount; r++)
            {
                colArray[r] = _matrix[r][col];
            }
            return colArray;
        }

        /// <summary>
        /// Sets a single value of the matrix. 
        /// </summary>
        /// <param name="rows">The index of the row.</param>
        /// <param name="cols">The index of the column.</param>
        /// <param name="value">The new value that will be set./param>
        public void Set(int row, int col, T value)
        {
            validate(row, col);
            _matrix[row][col] = value;
        }

        /// <summary>
        /// Checks, if the generic data type is a number or not.
        /// </summary>
        /// <returns>Returns 'true' T is a number, otherwise 'false'.</returns>
        public bool IsNumeric()
        {
            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns a string based on the number of rows an columns.
        /// </summary>
        /// <returns>The formatted string.</returns>
        public override string ToString() => $"Matrix: {RowsCount} x {ColsCount}";


        // --- private methods

        private void validate(int row, int col)
        {
            validateRow(row);
            validateCol(col);
        }

        private void validateRow(int row)
        {
            if (row < 0 || row > RowsCount)
            {
                throw new MatrixException("Die Zeile liegt außerhalb des gültigen Bereichs");
            }
        }

        private void validateCol(int col)
        {
            if (col < 0 || col > ColsCount)
            {
                throw new MatrixException("Die Spalte liegt außerhalb des gültigen Bereichs");
            }
        }

        // --- properties

        /// <summary>
        /// Gets the number of rows of the matrix. Set this with the Init method.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Gets the number of columns of the matrix. Set this with the Init method.
        /// </summary>
        public int ColsCount { get; private set; }

        // -- indexer 

        /// <summary>
        /// Gets a single element from the matrix after a validation of the input parameters.
        /// </summary>
        /// <param name="rows">The index of the row.</param>
        /// <param name="cols">The index of the column.</param>
        /// <returns>The value of the matrix.</returns>
        public T this[int row, int col]
        {
            get
            {
                validate(row, col);
                return _matrix[row][col];
            }
        }
    }
}
