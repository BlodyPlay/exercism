using System;

public static class SpiralMatrix
{
    public static int[,] GetMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        if (size <= 0)
        {
            return matrix;
        }

        int top = 0;
        int bottom = size - 1;
        int left = 0;
        int right = size - 1;
        int num = 1;

        while (top <= bottom && left <= right)
        {
            // Зліва направо по верхній межі
            for (int col = left; col <= right; col++)
            {
                matrix[top, col] = num++;
            }
            top++;

            // Зверху вниз по правій межі
            for (int row = top; row <= bottom; row++)
            {
                matrix[row, right] = num++;
            }
            right--;

            // Справа наліво по нижній межі
            if (top <= bottom)
            {
                for (int col = right; col >= left; col--)
                {
                    matrix[bottom, col] = num++;
                }
                bottom--;
            }

            // Знизу вгору по лівій межі
            if (left <= right)
            {
                for (int row = bottom; row >= top; row--)
                {
                    matrix[row, left] = num++;
                }
                left++;
            }
        }

        return matrix;
    }
}