using MvcCoreCrudDepartamentos.Models;
using System.Data.SqlClient;

#region PROCEDIMIENTOS_ALMACENADOS
//INSERTAR DEPARTAMENTO
    //CREATE PROCEDURE SP_INSERTDEPARTAMENTO
    //(@NOMBRE NVARCHAR(50), @LOCALIDAD NVARCHAR(50))
    //AS
    //    DECLARE @NEXTID INT
    //	SELECT @NEXTID = MAX(DEPT_NO) +1 FROM DEPT
    //	INSERT INTO DEPT VALUES (@NEXTID, @NOMBRE, @LOCALIDAD)
    //GO

#endregion



namespace MvcCoreCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryDepartamentos()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }


        //VAMOS A REALIZAR LAS CONSULTAS SOBRE LA BASE DE DATOS
        //DE FORMA ASINCRONA
        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string sql = "select * from DEPT";
            //Open(), ExecuteReader(),Read(),Close()
            this.com.CommandType= System.Data.CommandType.Text;
            this.com.CommandText= sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            List<Departamento> departamentos = new List<Departamento>();
            while (await this.reader.ReadAsync())
            {
                Departamento dept = new Departamento();
                dept.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();
                departamentos.Add(dept);
            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            string sql = "SELECT * FROM DEPT WHERE DEPT_NO=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText= sql;
            await this.cn.OpenAsync();
            this.reader = await this.com.ExecuteReaderAsync();
            //COMO ESTAMOS BUSCANDO, POR NORMA, SIEMPRE
            //DEBEMOS AVERIGUAR SI EXISTEN DATOS O NO EN EL REPOSITORY
            //SIEMPRE QUE NO EXISTAN DATOS EN LA BUSQUEDA, EL REPO
            //DEBE DEVOLVER NULL
            Departamento dept = null;
            if (await this.reader.ReadAsync())
            {
                //TENEMOS DATOS
                dept = new Departamento();
                dept.IdDepartamento = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();
            }
            else
            {
                //NO TENEMOS DATOS

            }
            await this.reader.CloseAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
            return dept;
        }

        public async Task InsertDepartamentoAsync(string nombre, string localidad)
        {
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@LOCALIDAD", localidad);
            this.com.CommandType= System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERTDEPARTAMENTO";
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            string sql = "UPDATE DEPT SET DNOMBRE = @NOMBRE, LOC = @LOCALIDAD WHERE DEPT_NO = @ID";
            this.com.Parameters.AddWithValue("@NOMBRE", nombre);
            this.com.Parameters.AddWithValue("@LOCALIDAD", localidad);
            this.com.Parameters.AddWithValue("@ID", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText= sql;
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();

        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            string sql = "DELETE FROM DEPT WHERE DEPT_NO=@id";
            this.com.Parameters.AddWithValue("@id", id);
            this.com.CommandType = System.Data.CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            int af = await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }
    }
}
