using D3vS1m.Application.Data;
using System;

namespace D3vS1m.Application.Network
{
    /// <summary>
    /// The network matrix is a special case of the matrix type. This matrix stores any type of data that represents a relationship between two network devices
    /// Consequently it has always the same number of rows an colums and
    /// during Each function calls the main diagonal is skipped, because it relates to the same device.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class NetworkMatrix<T> : Matrix<T>
    {
        // -- constructors

        public NetworkMatrix()
        {

        }

        public NetworkMatrix(int size)
        {
            Init(size);
        }

        // -- methods

        /// <summary>
        /// Initializes the matrix (new) and sets all items to its default(T) value.
        /// Existing values will be overwritten.
        /// </summary>
        /// <param name="size">The size of the matrix</param>
        public void Init(int size)
        {
            Size = size;
            base.Init(size, size);
        }

        /// <summary>
        /// Initializes the matrix (new) and sets all items to the given value.
        /// Existing values will be overwritten.
        /// </summary>
        /// <param name="size">The size of the matrix</param>
        /// <param name="value">the initial value for each item</param>
        public void Init(int size, T value)
        {
            Size = size;
            base.Init(size, size, value);
        }

        /// <summary>
        /// Führt eine beliebige Funktion für alle Matrix-Elemente aus. Die Hauptdiagonale wird ausgelassen.
        /// </summary>
        /// <param name="function">Die Delegatfunktion besitzt als Übergabe die Zeile [int], Spalte [int] und den aktuellen Wert vom Typ T der Matrix.
        /// Der Rückgabewert überschreibt das Matrixelement vom Typ T.</param>
        public sealed override void Each(Func<int, int, T, T> function)
        {
            for (int r = 0; r < base.RowsCount; r++)
            {
                for (int c = 0; c < base.ColsCount; c++)
                {
                    if (r == c) continue;
                    _matrix[r][c] = function(r, c, _matrix[r][c]);
                }
            }
        }

        // -- properties

        /// <summary>
        /// Gets the size of Matrix aus. Diese kann nur über die Init-Methode verändert werden.
        /// </summary>
        public int Size { get; private set; }
    }
}
