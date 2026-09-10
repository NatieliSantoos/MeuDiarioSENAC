namespace MeuDiarioSenac.Business;
using MeuDiarioSenac.Model;
using MeuDiarioSenac.Data;

public class RegistroBusiness
{
    public bool TituloFoiInformado(Registro registro)
    {   try
        {
            if (string.IsNullOrEmpty(registro.Titulo))
            {
                return false;

            }
            return true;
        }
        catch (Exception ex)
        {
           
            Console.WriteLine($"Erro ao validar título: {ex.Message}");
            return false;
        }
        
    }

    public bool TituloMaxChar(Registro registro)
    {   try
        {
            if (registro.Titulo.Length > 50)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao validar comprimento do título: {ex.Message}");
            return false;
        }
      
    }

    public bool DataAgora(Registro registro)
    { try
        {
            if (registro.Data > DateTime.Now)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao validar data: {ex.Message}");
            return false;
        }
       
    }

    public bool ConteudoMaxChar(Registro registro)
    {   try
        {
            if (registro.Conteudo.Length > 3000)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao validar comprimento do conteúdo: {ex.Message}");
            return false;
        }
        
    }
}