using DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DAO
{
    public class UsuarioDAO
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        
        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public UsuarioDTO getUsuarioById(int id_usuario)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    SqlDataReader reader = null;
                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        select * 
                        from Usuario 
                        where id_usuario = @pId_usuario
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_usuario", id_usuario);

                    reader = command.ExecuteReader();

                    UsuarioDTO usuario = new UsuarioDTO();

                    if (reader.Read())
                    {
                        usuario.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        usuario.Nome = reader["nome"].ToString();
                        usuario.Email = reader["email"].ToString();
                        usuario.Cidade = reader["cidade"].ToString();
                        usuario.Imagem = reader["imagem"].ToString();
                        usuario.Estado= reader["estado"].ToString();
                    }

                    conn.Close();

                    return usuario;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public UsuarioDTO AutenticaUsuario(string email, string senha)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    SqlDataReader reader = null;
                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        select *
                        from Usuario
                        where email = @pEmail
                        and senha = @pSenha
                    ");
                    
                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pEmail", email);
                    command.Parameters.AddWithValue("@pSenha", senha);

                    reader = command.ExecuteReader();

                    UsuarioDTO usuario = new UsuarioDTO();
                    
                    if (reader.Read())
                    {
                        usuario.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        usuario.Nome = reader["nome"].ToString();
                        usuario.Email = reader["email"].ToString();
                        usuario.Cidade = reader["cidade"].ToString();
                        usuario.Estado = reader["estado"].ToString();
                        usuario.Imagem = reader["imagem"].ToString();
                    }

                    conn.Close();

                    return usuario;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int VerificaUsuarioExistente(string email)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();
                    int retorno = 0;

                    sql.Append(@"
                        select COUNT(id_usuario)
                        from Usuario
                        where email = @pEmail
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pEmail", email);
                    
                    retorno = (int)command.ExecuteScalar();

                    conn.Close();

                    return retorno;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int SalvarUsuario(UsuarioDTO usuario)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    int id = 0;
                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        insert into Usuario (nome, email, cidade, imagem, senha, estado)
                        values (@pNome, @pEmail, @pCidade, @pImagem, @pSenha, @pEstado)
                        select @@identity
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pNome", usuario.Nome);
                    command.Parameters.AddWithValue("@pEmail", usuario.Email);
                    command.Parameters.AddWithValue("@pCidade", usuario.Cidade);
                    command.Parameters.AddWithValue("@pImagem", usuario.Imagem);
                    command.Parameters.AddWithValue("@pSenha", usuario.Senha);
                    command.Parameters.AddWithValue("@pEstado", usuario.Estado);

                    id = Convert.ToInt32(command.ExecuteScalar());

                    conn.Close();

                    return id;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool UpdateUsuario(UsuarioDTO usuario)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        update Usuario 
                        set
                        nome = @pNome, 
                        email = @pEmail, 
                        cidade = @pCidade,
                        estado = @pEstado,
                        imagem = @pImagem
                        where id_usuario = @pId_usuario
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_usuario", usuario.Id_usuario);

                    if (!string.IsNullOrEmpty(usuario.Nome))
                        command.Parameters.AddWithValue("@pNome", usuario.Nome);

                    if (!string.IsNullOrEmpty(usuario.Email))
                        command.Parameters.AddWithValue("@pEmail", usuario.Email);

                    if (!string.IsNullOrEmpty(usuario.Cidade))
                        command.Parameters.AddWithValue("@pCidade", usuario.Cidade);

                    if (!string.IsNullOrEmpty(usuario.Estado))
                        command.Parameters.AddWithValue("@pEstado", usuario.Estado);

                    if (!string.IsNullOrEmpty(usuario.Imagem))
                        command.Parameters.AddWithValue("@pImagem", usuario.Imagem);


                    command.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }

            }
            catch (SqlException ex)
            {
                return false;
            }
        }
    }
}