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
    public class ComentarioDAO
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public List<ComentarioDTO> GetComentariosByPublicacao(int id_publicacao)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    SqlDataReader reader = null;
                    StringBuilder sql = new StringBuilder();

                    conn.Open();

                    sql.Append(@"
                        select
                            c.id_comentario,
                            c.descricao,
                            c.data, 
                            c.id_publicacao, 
                            c.id_usuario,
	                        u.nome as nome_usuario,
                            u.imagem
                        from Comentario c
                        join Usuario u on c.id_usuario = u.id_usuario 
                        where id_publicacao = @pId_publicacao
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_publicacao", id_publicacao);

                    reader = command.ExecuteReader();

                    List<ComentarioDTO> list = new List<ComentarioDTO>();

                    while (reader.Read())
                    {
                        ComentarioDTO comentario = new ComentarioDTO();
                        comentario.Id_comentario = Convert.ToInt32(reader["id_comentario"]);
                        comentario.Descricao = reader["descricao"].ToString();
                        comentario.Data = (DateTime)reader["data"];
                        comentario.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        comentario.Nome_usuario = reader["nome_usuario"].ToString();
                        comentario.Avatar = reader["imagem"].ToString();
                        comentario.Id_publicacao = Convert.ToInt32(reader["id_publicacao"]);

                        list.Add(comentario);
                    }

                    conn.Close();

                    return list;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int SalvarComentario(ComentarioDTO comentario)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        insert into Comentario (descricao, data, id_publicacao, id_usuario)
                        values (@pDescricao, @pData, @pId_publicacao, @pId_usuario)
                        select @@identity
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pDescricao", comentario.Descricao);
                    command.Parameters.AddWithValue("@pData", comentario.Data);
                    command.Parameters.AddWithValue("@pId_publicacao", comentario.Id_publicacao);
                    command.Parameters.AddWithValue("@pId_usuario", comentario.Id_usuario);

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    conn.Close();

                    return id;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void ExcluirComentario(int id_comentario)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        delete from Comentario where id_comentario = @pId_comentario
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_comentario", id_comentario);

                    command.ExecuteNonQuery();

                    conn.Close();
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}