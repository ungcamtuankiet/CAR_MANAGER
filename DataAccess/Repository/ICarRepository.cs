using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
namespace DataAccess.Repository
{
    public interface ICarRepository
    {
        IEnumerable<CarObject> GetCarList();
        CarObject GetCar(int carID);
        IEnumerable<int> GetCarIDList();
        bool AddNewCar(CarObject car);
        bool UpdateCar(CarObject car);
        bool DeleteCar(int carID);
        IEnumerable<ManufacturerObject> GetManufacturerList();
        IEnumerable<CarTypeObject> GetCarTypeList();
        bool AddManufacturer(ManufacturerObject manu);
        bool UpdateManufacturer(ManufacturerObject manu);
        bool DeleteManufacturer(int manuID);
    }
}
