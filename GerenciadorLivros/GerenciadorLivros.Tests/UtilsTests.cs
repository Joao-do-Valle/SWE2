using GerenciadorLivros.Utils;

namespace GerenciadorLivros.Tests;

public class IsbnValidadorTests
{
    [Theory]
    [InlineData("9780306406157")]
    [InlineData("978-0-306-40615-7")]
    [InlineData("9780131103627")]
    [InlineData("0306406152")]
    [InlineData("080442957X")]
    public void EhValido_IsbnCorreto_RetornaTrue(string isbn)
    {
        Assert.True(IsbnValidador.EhValido(isbn));
    }

    [Theory]
    [InlineData("9780306406158")]
    [InlineData("0306406153")]
    [InlineData("123")]
    [InlineData("ABCDEFGHIJKLM")]
    [InlineData("")]
    [InlineData(null)]
    public void EhValido_IsbnIncorreto_RetornaFalse(string? isbn)
    {
        Assert.False(IsbnValidador.EhValido(isbn));
    }

    [Fact]
    public void Normalizar_RemoveHifensEEspacos()
    {
        Assert.Equal("9780306406157", IsbnValidador.Normalizar("978-0-306 40615-7"));
    }

    [Fact]
    public void Formatar_Isbn13_InsereHifenAposPrefixo()
    {
        Assert.Equal("978-0306406157", IsbnValidador.Formatar("9780306406157"));
    }
}

public class FormatadorTextoTests
{
    [Theory]
    [InlineData("o senhor dos aneis", "O Senhor dos Aneis")]
    [InlineData("  CLEAN   CODE ", "Clean Code")]
    [InlineData("dom casmurro", "Dom Casmurro")]
    [InlineData("", "")]
    public void TituloCapitalizado_FormataCorretamente(string entrada, string esperado)
    {
        Assert.Equal(esperado, FormatadorTexto.TituloCapitalizado(entrada));
    }
}

public class CalculadoraLeituraTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(-5, 0)]
    [InlineData(30, 1)]
    [InlineData(31, 2)]
    [InlineData(300, 10)]
    public void HorasEstimadas_CalculaArredondandoParaCima(int paginas, int esperado)
    {
        Assert.Equal(esperado, CalculadoraLeitura.HorasEstimadas(paginas));
    }

    [Theory]
    [InlineData(0, "Não informado")]
    [InlineData(20, "Cerca de 1 hora")]
    [InlineData(90, "Cerca de 3 horas")]
    public void DescricaoTempo_RetornaTextoAmigavel(int paginas, string esperado)
    {
        Assert.Equal(esperado, CalculadoraLeitura.DescricaoTempo(paginas));
    }
}
