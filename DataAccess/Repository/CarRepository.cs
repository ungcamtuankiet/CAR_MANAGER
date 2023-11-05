using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CarRepository : ICarRepository
    {
        private CarDAO instance = CarDAO.Instance;
        public IEnumerable<CarObject> GetCarList() => instance.GetCarList();
        public IEnumerable<int> GetCarIDList() => instance.GetCarIDList();
        public CarObject GetCar(int carID) => instance.GetCar(carID);
        public bool AddNewCar(CarObject car) => instance.AddNewCar(car);
        public bool UpdateCar(CarObject car) => instance.UpdateCar(car);
        public bool DeleteCar(int carID) => instance.DeleteCar(carID);
        public IEnumerable<ManufacturerObject> GetManufacturerList() => instance.GetManufacturerList();
        public IEnumerable<CarTypeObject> GetCarTypeList() => instance.GetCarTypeList();
        public bool AddManufacturer(ManufacturerObject manu) => instance.AddManufacturer(manu);
        public bool UpdateManufacturer(ManufacturerObject manu) => instance.UpdateManufacturer(manu);
        public bool DeleteManufacturer(int manuID) => instance.DeleteManufacturer(manuID);
    }
}
