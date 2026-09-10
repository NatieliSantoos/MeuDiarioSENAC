using MeuDiarioSenac.Data;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Service;

public class RegistroService
{
	private readonly RegistroDAO _registroDAO;

	public RegistroService(RegistroDAO registroDAO)
	{
		_registroDAO = registroDAO;
	}

	public void CadastrarRegistro(Registro registro)
	{
		_registroDAO.CadastrarRegistro(registro);
	}

	public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
	{
		return _registroDAO.ListarRegistrosPorUsuario(usuarioId);
	}

	public Registro? ObterRegistroPorId(int id, int usuarioId)
	{
		return _registroDAO.ObterRegistroPorId(id, usuarioId);
	}

	public void RemoverRegistro(int id, int usuarioId)
	{
		_registroDAO.RemoverRegistro(id, usuarioId);
	}
}
