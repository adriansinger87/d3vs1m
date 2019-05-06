using System;

namespace D3vS1m.Application.Data
{
    // TODO: make english comments
    [Serializable]
    public class Matrix<T>
    {

        /*
         * generic jagged array:
         * KEIN 2D-Array, weil diese langsamer sind, in manchen Fällen innere Schleifen benötigen, wo eine jagged array keins braucht UND
         * weil man leicht eine 3-Ecks Matrix erstellen kann, da es keine zeilenweisen Abhängigkeiten gibt.
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
        /// Initialisiert die Matrix (neu) und besetzt alle Elemente mit dem default(T) Wert des festgelegten Typs.
        /// Beim Aufrufrbei werden eventuell bereits existierende Elemente gelöscht.
        /// </summary>
        /// <param name="rows">die Anzahl der Zeilen der Matrix</param>
        /// <param name="cols">die Anzahl der Spalten der Matrix</param>
        public void Init(int rows, int cols)
        {
            Init(rows, cols, default(T));
        }

        /// <summary>
        ///  Initialisiert die Matrix (neu) und besetzt alle Elemente mit dem übergebenen Wert value.
        /// Beim Aufrufrbei werden eventuell bereits existierende Elemente gelöscht.
        /// </summary>
        /// <param name="rows">die Anzahl der Zeilen der Matrix</param>
        /// <param name="cols">die Anzahl der Spalten der Matrix</param>
        /// <param name="value">der initiale Wert</param>
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
        /// Führt eine beliebige Funktion für alle Matrix-Elemente aus.
        /// </summary>
        /// <param name="function">Die Delegatfunktion besitzt als Übergabe die Zeile [int], Spalte [int] und den aktuellen Wert vom Typ T der Matrix.
        /// Der Rückgabewert überschreibt das Matrixelement vom Typ T.</param>
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
        /// Gibt einen einzelnen Wert der Matrix aus. Die Hauptdiagonale beinhaltet stets die default(T) Werte des festgelegten Typs.
        /// </summary>
        /// <param name="row">Die Zeile</param>
        /// <param name="col">Die Spalte</param>
        /// <returns>Der Rückgabewert vom Typ der Matrix</returns>
        public T Get(int row, int col)
        {
            validate(row, col);
            return _matrix[row][col];
        }

        public T[] GetRow(int row)
        {
            validateRow(row);
            return _matrix[row];
        }

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
        /// Ruft einen einzelnen Wert der Matrix ab. Die Hauptdiagonale beinhaltet stets die default(T) Werte des festgelegten Typs.
        /// </summary>
        /// <param name="row">Die Zeile</param>
        /// <param name="col">Die Spalte</param>
        /// <param name="value">Der zu setzende Wert vom Typ der Matrix</param>
        public void Set(int row, int col, T value)
        {
            validate(row, col);
            _matrix[row][col] = value;
        }

        /// <summary>
        /// Prüft, ob der generische Datentyp eine Zahl ist.
        /// </summary>
        /// <returns>Bei Erfolg 'true', andernfalls 'false'</returns>
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

        public override string ToString()
        {
            return string.Format("Rows: {0} x Cols: {1} Matrix", RowsCount, ColsCount);
        }

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
                throw new Exception("Die Zeile liegt außerhalb des gültigen Bereichs");
            }
        }

        private void validateCol(int col)
        {
            if (col < 0 || col > ColsCount)
            {
                throw new Exception("Die Spalte liegt außerhalb des gültigen Bereichs");
            }
        }

        // --- properties

        /// <summary>
        /// Gibt die Größe der Matrix aus. Diese kann nur über die Init-Methode verändert werden.
        /// </summary>
        public int RowsCount { get; private set; }

        /// <summary>
        /// Gibt die Größe der Matrix aus. Diese kann nur über die Init-Methode verändert werden.
        /// </summary>
        public int ColsCount { get; private set; }

        // -- indexer 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public T this[int row, int col]
        {
            get
            {
                return _matrix[row][col];
            }
        }

    }
}
