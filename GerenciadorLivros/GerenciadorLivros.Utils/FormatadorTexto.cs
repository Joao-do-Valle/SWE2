using System.Globalization;

namespace GerenciadorLivros.Utils;

/// <summary>Utilitários de formatação de texto para títulos e nomes de autores.</summary>
public static class FormatadorTexto
{
    private static readonly HashSet<string> Conectivos = new(StringComparer.OrdinalIgnoreCase)
    {
        "de", "da", "do", "das", "dos", "e", "a", "o", "as", "os", "em", "para", "com"
    };

    /// <summary>
    /// Coloca a primeira letra de cada palavra em maiúscula, mantendo
    /// conectivos (de, da, do, e...) em minúscula, exceto na primeira palavra.
    /// Ex.: "o senhor dos aneis" => "O Senhor dos Aneis".
    /// </summary>
    public static string TituloCapitalizado(string? texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return string.Empty;

        var cultura = new CultureInfo("pt-BR");
        var palavras = texto.Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select((palavra, indice) =>
            {
                var minuscula = palavra.ToLower(cultura);

                if (indice > 0 && Conectivos.Contains(minuscula))
                    return minuscula;

                return char.ToUpper(minuscula[0], cultura) + minuscula[1..];
            });

        return string.Join(' ', palavras);
    }
}
