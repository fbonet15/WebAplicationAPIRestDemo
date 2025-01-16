using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using WebAplicationAPIRestDemo.DAL.Model;
using WebApplicationAPIDemo.Model;
using WebApplicationAPIDemo.Model;
using WebApplicationAPIDemo.Persistence;

namespace WebAplicationAPIRestDemo.DAL.Service
{
    public class ItemKanBanService
    {
        public List<ItemKanBan> GetAll()
        {
            var result = new List<ItemKanBan>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM ItemKanBan";

                using (var command = new SQLiteCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ItemKanBan
                            {
                                id = (long)reader["id"],
                                tasca = reader["tasca"].ToString(),
                                estat = reader["estat"].ToString(),
                                color = reader["color"].ToString(),
                                dataStart = reader["dataStart"].ToString(),
                                dataFinish = reader["dataFinish"].ToString(),
                                Responsable = ObtenerResponsablePorId(int.Parse(reader["Responsable"].ToString()))

                            });
                        }
                    }
                }
            }
            return result;
        }

        public ItemKanBan Add(ItemKanBan item)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO ItemKanBan (tasca, estat, color, dataStart, dataFinish, Responsable) VALUES (@tasca, @estat, @color, @dataStart, @dataFinish, @Responsable)";
                using (var command = new System.Data.SQLite.SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("tasca", item.tasca));
                    command.Parameters.Add(new SQLiteParameter("estat", item.estat));
                    command.Parameters.Add(new SQLiteParameter("color", item.color));
                    command.Parameters.Add(new SQLiteParameter("dataStart", item.dataStart));
                    command.Parameters.Add(new SQLiteParameter("dataFinish", item.dataFinish));
                    command.Parameters.Add(new SQLiteParameter("Responsable", item.Responsable.id));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    item.id = (Int64)command.ExecuteScalar();
                }
            }

            return item;
        }

        public int Update(ItemKanBan item)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE ItemKanBan SET tasca = @tasca, estat = @estat, color = @color, dataStart = @dataStart, dataFinish = @dataFinish, Responsable = @Responsable WHERE id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("tasca", item.tasca));
                    command.Parameters.Add(new SQLiteParameter("estat", item.estat));
                    command.Parameters.Add(new SQLiteParameter("color", item.color));
                    command.Parameters.Add(new SQLiteParameter("dataStart", item.dataStart));
                    command.Parameters.Add(new SQLiteParameter("dataFinish", item.dataStart));
                    command.Parameters.Add(new SQLiteParameter("Responsable", item.Responsable));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }
        public int Delete(long id)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM ItemKanBan WHERE id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        public Responsable ObtenerResponsablePorId(int id)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "SELECT * FROM Responsables WHERE Id = @id";
                using (var command = new SQLiteCommand(query, ctx))
                {
                    command.Parameters.Add(new SQLiteParameter("id", id));
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Responsable
                            {
                                id = reader.GetInt32("id"),
                                nom = reader.GetString("nom"),
                                cognom = reader.GetString("cognom"),
                                correu = reader.GetString("correu")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
