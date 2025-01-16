using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationAPIDemo.Model;
using WebApplicationAPIDemo.Persistence;

namespace WebApplicationAPIDemo.DAL.Service
{
    public class ResponsableService
    {
        /// <summary>
        /// Obté tots els usuaris
        /// </summary>
        /// <returns></returns>
        public List<Responsable> GetAll()
        {
            var result = new List<Responsable>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM responsables";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Responsable
                            {
                                id = (long)reader["id"],
                                nom = reader["nom"].ToString(),
                                cognom = reader["cognom"].ToString(),
                                correu = reader["correu"].ToString(),
                            });
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Obté les dades de l'usuari indicat
        /// </summary>
        /// <param name="id">Identificador d'usuari</param>
        /// <returns>Dades de l'Usuari</returns>
        public Responsable GetById(double id)
        {
            Responsable responsable = null;

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Responsables WHERE id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            responsable = new Responsable()
                            {
                                id = Convert.ToInt64(reader["id"].ToString()),
                                nom = reader["nom"].ToString(),
                                cognom = reader["cognom"].ToString(),
                                correu = reader["correu"].ToString(),
                            };
                        }
                    }
                }
            }
            return responsable;
        }

        /// <summary>
        /// Afegeix un nou usuari a la base de dades
        /// </summary>
        /// <param name="responsable">Entitat usuari</param>
        /// <returns>Id de l'usuari afegit</returns>
        public Responsable Add(Responsable responsable)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Responsables (nom, cognom, id, correu) VALUES (@nom, @cognom, @id, @correu)";
                using (var command = new System.Data.SQLite.SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nom", responsable.nom));
                    command.Parameters.Add(new SQLiteParameter("cognom", responsable.cognom));
                    command.Parameters.Add(new SQLiteParameter("id", responsable.id));
                    command.Parameters.Add(new SQLiteParameter("correu", responsable.correu));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    responsable.id = (Int64) command.ExecuteScalar();
                }
            }

            return responsable;
        }

        /// <summary>
        /// Actualitza un usuari
        /// </summary>
        /// <param name="responsable">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int Update(Responsable responsable)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE Responsables SET nom = @nom, cognom = @cognom, edat = @edat WHERE id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("nom", responsable.nom));
                    command.Parameters.Add(new SQLiteParameter("cognom", responsable.cognom));
                    command.Parameters.Add(new SQLiteParameter("id", responsable.id));
                    command.Parameters.Add(new SQLiteParameter("correu", responsable.correu));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Elimina un usuari
        /// </summary>
        /// <param name="id">Codi d'usuari que es vol eliminar</param>
        /// <returns>Files afectades</returns>
        public int Delete(long id)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Responsables WHERE id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }
    }
}
