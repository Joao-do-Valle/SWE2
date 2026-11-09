namespace GerenciadorLivros.Utils;

/// <summary>Cálculos relacionados à leitura de um livro.</summary>
public static class CalculadoraLeitura
{
    /// <summary>Média de páginas lidas por hora usada na estimativa.</summary>
    public const int PaginasPorHora = 30;

    /// <summary>Estima o tempo de leitura em horas (arredondado para cima).</summary>
    public static int HorasEstimadas(int numeroPaginas)
    {
        if (numeroPaginas <= 0)
            return 0;

        return (int)Math.Ceiling(numeroPaginas / (double)PaginasPorHora);
    }

    /// <summary>Retorna uma descrição amigável, como "Cerca de 5 horas".</summary>
    public static string DescricaoTempo(int numeroPaginas)
    {
        var horas = HorasEstimadas(numeroPaginas);

        return horas switch
        {
            0 => "Não informado",
            1 => "Cerca de 1 hora",
            _ => $"Cerca de {horas} horas"
        };
    }
}
