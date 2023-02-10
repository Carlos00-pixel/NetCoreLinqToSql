using NetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace NetCoreLinqToSql.Repositories
{
    public class RepositoryEmpleados
    {
        private DataTable tablaEmpleados;

        public RepositoryEmpleados()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023;TrustServerCertificate=True";
            string sql = "SELECT * FROM EMP";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaEmpleados = new DataTable();
            adapter.Fill(this.tablaEmpleados);
        }

        //METODO PARA RECUPERAR TODOS LOS EMPLEADOS
        public List<Empleado> GetEmpleados()
        {
            //LA TABLA ESTA COMPUESTA POR FILAS (DataRow)
            //LA CONSULTA DEBE SER SOBRE TODAS LAS FILAS DE
            //LA TABLA
            var consulta = from datos in this.tablaEmpleados.AsEnumerable() 
                           select datos;
            //AHORA MISMO TENEMOS EN CONSULTA UNA COLECCION LINQ
            //DE OBJETOS DataRow
            //QUE PODEMOS ORDENAR, FILTRAR Y HACER TODO LO QUE DESEEMOS
            List<Empleado> empleados = new List<Empleado>();
            //VAMOS A RECORRER TODOS LOS DATOS DE LA CONSULTA Y EXTRAERLOS
            foreach (var row in consulta)
            {
                Empleado empleado = new Empleado
                {
                    IdEmpleado = row.Field<int>("EMP_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Oficio = row.Field<string>("OFICIO"),
                    Salario = row.Field<int>("SALARIO"),
                    IdDepatamento = row.Field<int>("DEPT_NO")
                };
                empleados.Add(empleado);
            }
            return empleados;
        }

        public Empleado FindEmpleado(int idempleado)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<int>("EMP_NO") == idempleado
                           select datos;
            //NOSOTROS SABEMOS QUE DEVUELVE SOLAMENTE UNA FILA
            //PERO CONSULTA NO LO SABE
            //consulta CONTIENE UNA SERIE DE METODOS PARA TRATAR
            //LOS DATOS O RECUPERARLOS O REALIZAR CIERTAS ACCIONES
            //TENEMOS UN METODO PARA RECUPERAR LA PRIMERA FILA
            //DE LA CONSULTA .First()
            var row = consulta.First();
            Empleado empleado = new Empleado
            {
                IdEmpleado = row.Field<int>("EMP_NO"),
                Apellido = row.Field<string>("APELLIDO"),
                Oficio = row.Field<string>("OFICIO"),
                Salario = row.Field<int>("SALARIO"),
                IdDepatamento = row.Field<int>("DEPT_NO")
            };
            return empleado;
        }

        public List<Empleado> GetEmpleados(string oficio, int salario)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                           where datos.Field<string>("OFICIO") == oficio
                           && datos.Field<int>("SALARIO") >= salario
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Empleado> empleados = new List<Empleado>();
                foreach (var row in consulta)
                {
                    Empleado empleado = new Empleado
                    {
                        IdEmpleado = row.Field<int>("EMP_NO"),
                        Apellido = row.Field<string>("APELLIDO"),
                        Oficio = row.Field<string>("OFICIO"),
                        Salario = row.Field<int>("SALARIO"),
                        IdDepatamento = row.Field<int>("DEPT_NO")
                    };
                    empleados.Add(empleado);
                }
                return empleados;
            }
        }
    }
}
