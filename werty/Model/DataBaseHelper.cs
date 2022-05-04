using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace werty.Model
{
    public static class DataBaseHelper
    {
        private static DrafrEntities _Entities = new DrafrEntities();

        public static List <Material> GetMaterials()
        {
            return _Entities.Material.ToList();
        }
        public static List<MaterialType> GetMaterialsType()
        {
            return _Entities.MaterialType.ToList();
        }

        public static void SaveChanges()
        {
            _Entities.SaveChanges();
        }
    }
}
