namespace Backend.Shared.Other
{
    public static class DividingWithRoundingUpExtension
    {
        public static int DivideWithRoundingUp(this int dividend, int divisor)
        {
            // Обработка деления на ноль
            if (divisor == 0) throw new DivideByZeroException("Divisor cannot be zero.");

            // Вычисляем результат деления
            int result = dividend / divisor;

            // Если остаток от деления не равен нулю, увеличиваем результат на 1
            if (dividend % divisor != 0) result++;

            return result;
        }
    }
}