namespace Utilities.Generators;

/// <summary>
/// Gera idade a partir de data de nascimento
/// </summary>
public static class AgeGenerator
{
    /// <summary>
    /// Calcula a idade com base em uma data de nascimento e data da idade.
    /// </summary>
    /// <param name="dateOfBirth">Data de nascimento</param>
    /// <param name="today">Data atual para a idade</param>
    /// <returns>Idade em anos completos</returns>
    public static int Get(DateTime dateOfBirth, DateTime? today = null)
    {
        var current = today?.Date ?? DateTime.Today;

        if (dateOfBirth == DateTime.MinValue)
        {
            return 0;
        }

        int age = current.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > current.AddYears(-age))
            age--;

        return age;
    }
}
