using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace werty.Model
{
    public partial class Material
    {
        public string Suppliers { get
            {
                var titles = String.Join(", ", Supplier.Select(s => s.Title).ToList());
                if (titles.Length == 0)
                {
                    return "Нет поставщиков";
                }
                else
                {
                    return String.Join(", ", Supplier.Select(s => s.Title).ToList());
                }
                
            } 
        }


        public string ImagePath { get
            {
                var image = Image;
                if (image is null)
                {
                    return "/Image/picture.png";
                }
                else
                {
                    var ImagePath = Directory.GetCurrentDirectory() + Image.ToString();
                    return ImagePath;
                }
                
            } }

        public object Background
        {
            get
            {
                var min = MinCount;
                var inStock = CountInStock;
                if (inStock < min)
                {
                    return new BrushConverter().ConvertFrom("#f19292");
                }
                else if (min *3 < inStock)
                {
                    return new BrushConverter().ConvertFrom("#ffba01");
                }
                else
                {
                    return new BrushConverter().ConvertFrom("#ffffff");
                }
            }
        }

       
    }
}
