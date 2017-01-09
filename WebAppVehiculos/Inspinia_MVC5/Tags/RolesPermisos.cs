using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspinia_MVC5.Tags
{
    public enum RolesPermisos
    {
        #region Alumnos
        Alumno_Puede_Crear_Nuevo_Registro = 2,
        Alumno_Puede_Eliminar_Registro = 3,
        Alumno_Puede_Visualizar_Un_Alumno = 4,
        #endregion
    }
}