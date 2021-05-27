using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCam.DependencyServices
{
    public interface ICamera
    {
       Task<byte[]> GetPreviewFromView();
       

    }
}
