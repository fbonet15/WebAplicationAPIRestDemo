using System;
using System.Collections.Generic;
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
                                _id = (long)reader["id"],
                                _tasca = reader["tasca"].ToString(),
                                _estat = reader["estat"].ToString(),
                                _color = reader["color"].ToString(),
                                _dataStart = reader["dataStart"].ToString(),
                                _dataFinish = reader["dataFinish"].ToString(),
                                //això no anirà ni de conya
                                _responsable = (Responsable)reader["Responsable"]
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
                    command.Parameters.Add(new SQLiteParameter("tasca", item._tasca));
                    command.Parameters.Add(new SQLiteParameter("estat", item._estat));
                    command.Parameters.Add(new SQLiteParameter("color", item._color));
                    command.Parameters.Add(new SQLiteParameter("dataStart", item._dataStart));
                    command.Parameters.Add(new SQLiteParameter("dataFinish", item._dataStart));
                    command.Parameters.Add(new SQLiteParameter("Responsable", item._responsable));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    item._id = (Int64)command.ExecuteScalar();
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
                    command.Parameters.Add(new SQLiteParameter("tasca", item._tasca));
                    command.Parameters.Add(new SQLiteParameter("estat", item._estat));
                    command.Parameters.Add(new SQLiteParameter("color", item._color));
                    command.Parameters.Add(new SQLiteParameter("dataStart", item._dataStart));
                    command.Parameters.Add(new SQLiteParameter("dataFinish", item._dataStart));
                    command.Parameters.Add(new SQLiteParameter("Responsable", item._responsable));

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
    }
}
