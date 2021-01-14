using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCam.DependencyServices
{
    public interface ICamera
    {
        byte[] GetPreviewFromView();

    }
}
