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
    public class PublicacaoDAO
    {
        private string ConnectionString = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public List<PublicacaoDTO> GetAllPublicacoes()
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
                            p.id_publicacao, 
                            p.id_usuario, 
                            p.titulo, 
                            p.descricao, 
                            p.cidade, 
                            p.estado, 
                            p.data_publicacao, 
                            p.status, 
                            p.imagem, 
                            p.localizacao,
                            p.likes,
                            u.nome as nome_usuario,
                            u.imagem as imagem_usuario
                        from Publicacao p
                        join Usuario u on p.id_usuario = u.id_usuario
                        where 1 = 1
                        order by data_publicacao desc
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    reader = command.ExecuteReader();

                    List<PublicacaoDTO> list = new List<PublicacaoDTO>();

                    while (reader.Read())
                    {
                        PublicacaoDTO publicacao = new PublicacaoDTO();

                        publicacao.Id_publicacao = Convert.ToInt32(reader["id_publicacao"]);
                        publicacao.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        publicacao.Titulo = reader["titulo"].ToString();
                        publicacao.Descricao = reader["descricao"].ToString();
                        publicacao.Cidade = reader["cidade"].ToString();
                        publicacao.Estado = reader["estado"].ToString();
                        publicacao.Data_publicacao = (DateTime)reader["data_publicacao"];
                        publicacao.Status = Convert.ToBoolean(reader["status"]);
                        publicacao.Imagem = reader["imagem"].ToString();
                        publicacao.Localizacao = reader["localizacao"].ToString();
                        publicacao.Likes = Convert.ToInt32(reader["likes"]);
                        publicacao.Nome_usuario = reader["nome_usuario"].ToString();
                        publicacao.Imagem_usuario = reader["imagem_usuario"].ToString();

                        list.Add(publicacao);
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

        public List<PublicacaoDTO> GetPublicacoesByUsuario(int id_usuario)
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
                            p.id_publicacao, 
                            p.id_usuario, 
                            p.titulo, 
                            p.descricao, 
                            p.cidade, 
                            p.estado, 
                            p.data_publicacao, 
                            p.status, 
                            p.imagem, 
                            p.localizacao,
                            p.likes
                        from Publicacao p
                        where 1 = 1
                        and id_usuario = @pId_usuario
                        order by data_publicacao desc
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    if(id_usuario > 0)
                        command.Parameters.AddWithValue("@pId_usuario", id_usuario);

                    reader = command.ExecuteReader();

                    List<PublicacaoDTO> list = new List<PublicacaoDTO>();

                    while (reader.Read())
                    {
                        PublicacaoDTO publicacao = new PublicacaoDTO();

                        publicacao.Id_publicacao = Convert.ToInt32(reader["id_publicacao"]);
                        publicacao.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        publicacao.Titulo = reader["titulo"].ToString();
                        publicacao.Descricao = reader["descricao"].ToString();
                        publicacao.Cidade = reader["cidade"].ToString();
                        publicacao.Estado = reader["estado"].ToString();
                        publicacao.Data_publicacao = (DateTime)reader["data_publicacao"];
                        publicacao.Status = Convert.ToBoolean(reader["status_publicacao"]);
                        publicacao.Imagem = reader["imagem"].ToString();
                        publicacao.Localizacao = reader["localizacao"].ToString();
                        publicacao.Likes = Convert.ToInt32(reader["likes"]);

                        list.Add(publicacao);
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

        public PublicacaoDTO GetPublicacaoById(int id_publicacao)
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
                            p.id_publicacao, 
                            p.id_usuario, 
                            p.titulo, 
                            p.descricao, 
                            p.cidade, 
                            p.estado, 
                            p.data_publicacao, 
                            p.status_publicacao, 
                            p.imagem, 
                            p.localizacao,
                            p.likes,
                            u.nome as nome_usuario,
                            u.imagem as imagem_usuario
                        from Publicacao p
                        join Usuario u on p.id_usuario = u.id_usuario
                        where 1 = 1
                        and id_publicacao = @pId_publicacao
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_publicacao", id_publicacao);

                    reader = command.ExecuteReader();

                    PublicacaoDTO publicacao = new PublicacaoDTO();

                    while (reader.Read())
                    {
                        publicacao.Id_publicacao = Convert.ToInt32(reader["id_publicacao"]);
                        publicacao.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        publicacao.Titulo = reader["titulo"].ToString();
                        publicacao.Descricao = reader["descricao"].ToString();
                        publicacao.Cidade = reader["cidade"].ToString();
                        publicacao.Estado = reader["estado"].ToString();
                        publicacao.Data_publicacao = (DateTime)reader["data_publicacao"];
                        publicacao.Status = Convert.ToBoolean(reader["status_publicacao"]);
                        publicacao.Imagem = reader["imagem"].ToString();
                        publicacao.Localizacao = reader["localizacao"].ToString();
                        publicacao.Likes = Convert.ToInt32(reader["likes"]);
                        publicacao.Nome_usuario = reader["nome_usuario"].ToString();
                        publicacao.Imagem_usuario = reader["imagem_usuario"].ToString();
                    }

                    conn.Close();

                    return publicacao;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int SalvarPublicacao(PublicacaoDTO publicacao)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        insert into Publicacao (id_usuario, titulo, descricao, cidade, estado, data_publicacao, status_publicacao, imagem, localizacao, likes)
                        values (@pId_usuario, @pTitulo, @pDescricao, @pCidade, @pEstado, @pData_publicacao, @pStatus_publicacao, @pImagem, @pLocalizacao, @pLikes)
                        select @@identity
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pId_usuario", publicacao.Id_usuario);
                    command.Parameters.AddWithValue("@pTitulo", publicacao.Titulo);
                    command.Parameters.AddWithValue("@pDescricao", publicacao.Descricao);
                    command.Parameters.AddWithValue("@pCidade", publicacao.Cidade);
                    command.Parameters.AddWithValue("@pEstado", publicacao.Estado);
                    command.Parameters.AddWithValue("@pData_publicacao", publicacao.Data_publicacao);
                    command.Parameters.AddWithValue("@pStatus_publicacao", publicacao.Status);
                    command.Parameters.AddWithValue("@pImagem", publicacao.Imagem);
                    command.Parameters.AddWithValue("@pLocalizacao", publicacao.Localizacao);
                    command.Parameters.AddWithValue("@pLikes", 0);

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

        public void UpdateLike(int id_publicacao, int likes)
        {
            try
            {
                using (var conn = GetSqlConnection())
                {
                    conn.Open();

                    StringBuilder sql = new StringBuilder();

                    sql.Append(@"
                        update Publicacao set likes = @pLikes where id_publicacao = @pId_publicacao
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    command.Parameters.AddWithValue("@pLikes", likes);
                    command.Parameters.AddWithValue("@pId_publicacao", id_publicacao);

                    command.ExecuteNonQuery();

                    conn.Close();
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<PublicacaoDTO> GetPublicacoesByCidade(string cidade)
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
                            p.id_publicacao, 
                            p.id_usuario, 
                            p.titulo, 
                            p.descricao, 
                            p.cidade, 
                            p.estado, 
                            p.data_publicacao, 
                            p.status_publicacao, 
                            p.imagem, 
                            p.localizacao,
                            p.likes,
                            u.nome as nome_usuario,
                            u.imagem as imagem_usuario
                        from Publicacao p
                        join Usuario u on p.id_usuario = u.id_usuario
                        where 1 = 1
                        and p.cidade like @pCidade
                        order by data_publicacao desc
                    ");

                    SqlCommand command = conn.CreateCommand();

                    command.CommandText = sql.ToString();

                    if (!string.IsNullOrEmpty(cidade))
                        command.Parameters.AddWithValue("@pCidade", "%" + cidade + "%");

                    reader = command.ExecuteReader();

                    List<PublicacaoDTO> list = new List<PublicacaoDTO>();

                    while (reader.Read())
                    {
                        PublicacaoDTO publicacao = new PublicacaoDTO();

                        publicacao.Id_publicacao = Convert.ToInt32(reader["id_publicacao"]);
                        publicacao.Id_usuario = Convert.ToInt32(reader["id_usuario"]);
                        publicacao.Titulo = reader["titulo"].ToString();
                        publicacao.Descricao = reader["descricao"].ToString();
                        publicacao.Cidade = reader["cidade"].ToString();
                        publicacao.Estado = reader["estado"].ToString();
                        publicacao.Data_publicacao = (DateTime)reader["data_publicacao"];
                        publicacao.Status = Convert.ToBoolean(reader["status"]);
                        publicacao.Imagem = reader["imagem"].ToString();
                        publicacao.Localizacao = reader["localizacao"].ToString();
                        publicacao.Likes = Convert.ToInt32(reader["likes"]);
                        publicacao.Nome_usuario = reader["nome_usuario"].ToString();
                        publicacao.Imagem_usuario = reader["imagem_usuario"].ToString();

                        list.Add(publicacao);
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
    }
}