using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess
{
    public class CarDAO : BaseDAO
    {
        private CarDAO(): base() { }
        private static CarDAO instance = null;
        private static readonly object instanceLock = new object();
        public static CarDAO Instance
        {
            get
            { 
                lock(instanceLock)
                {
                    return instance == null ? (instance = new CarDAO()) : instance;
                }
            }
        }
        public IEnumerable<CarObject> GetCarList()
        {
            List<CarObject> list = new List<CarObject>();
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT CarID, CarModel, CarName, Price, LoadCapacity, SeatNum, FuelCapacity, CarType, TypeName, Unit, Manufacturer, ManuName, car.Status, TotalQuantity " +
                        " FROM (Cars car JOIN CarTypes type ON car.CarType = type.TypeID) JOIN Manufacturers manu on car.Manufacturer = manu.ManuID " +
                        " WHERE car.Status = 1 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        if (reader!= null)
                        {
                            while(reader.Read())
                            {
                                var car = new CarObject()
                                {
                                    CarId = reader.GetInt32(0),
                                    CarModel = reader.GetString(1),
                                    CarName = reader.GetString(2),
                                    Price = reader.GetDecimal(3),
                                    LoadCapacity = reader.GetInt32(4),
                                    SeatNum = reader.GetInt32(5),
                                    FuelCapacity = reader.GetInt32(6),
                                    CarType = reader.GetByte(7),
                                    TypeName = reader.GetString(8),
                                    Unit = reader.GetString(9),
                                    Manufacturer = reader.GetInt16(10),
                                    ManuName = reader.GetString(11),
                                    Status = reader.GetBoolean(12),
                                    TotalQuantity = reader.GetInt32(13)
                                };
                                list.Add(car);
                            }
                        }
                    }
                }
            } catch (Exception)
            {

            }
            return list;
        }

        public CarObject GetCar(int carID)
        {
            CarObject car = null;
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT CarModel, CarName, Price, LoadCapacity, SeatNum, FuelCapacity, CarType, TypeName, Unit, Manufacturer, ManuName, car.Status, TotalQuantity " +
                        " FROM (Cars car JOIN CarTypes type ON car.CarType = type.TypeID) JOIN Manufacturers manu on car.Manufacturer = manu.ManuID " +
                        " WHERE CarID = @carID AND car.Status = 1 AND TotalQuantity > 0 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection, new SqlParameter("@carID", carID)))
                    {
                        if (reader.Read())
                        {
                            car = new CarObject()
                            {
                                CarId = carID,
                                CarModel = reader.GetString(0),
                                CarName = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                LoadCapacity = reader.GetInt32(3),
                                SeatNum = reader.GetInt32(4),
                                FuelCapacity = reader.GetInt32(5),
                                CarType = reader.GetByte(6),
                                TypeName = reader.GetString(7),
                                Unit = reader.GetString(8),
                                Manufacturer = reader.GetInt16(9),
                                ManuName = reader.GetString(10),
                                Status = reader.GetBoolean(11),
                                TotalQuantity = reader.GetInt32(12)
                            };
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
            return car;
        }
        public List<ManufacturerObject> GetManufacturerList()
        {
            List<ManufacturerObject> list = new();
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT ManuID, ManuName, Status " +
                        " FROM Manufacturers " +
                        " WHERE Status = 1 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        while (reader.Read()) 
                        {
                            var manu = new ManufacturerObject()
                            {
                                ManuID = reader.GetInt16(0),
                                ManuName = reader.GetString(1),
                                Status = reader.GetBoolean(2)
                            };
                            list.Add(manu);
                        }
                    }
                }
            } 
            catch (Exception)
            {

            }
            return list;
        }

        public List<CarTypeObject> GetCarTypeList()
        {
            List<CarTypeObject> list = new();
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT TypeID, TypeName, Unit" +
                        " FROM CarTypes ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        while (reader.Read())
                        {
                            var type = new CarTypeObject()
                            {
                                TypeID = reader.GetByte(0),
                                TypeName = reader.GetString(1),
                                Unit = reader.GetString(2)
                            };
                            list.Add(type);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }
        public List<int> GetCarIDList()
        {
            List<int> carIDList = new();
            try
            {
                using (connection)
                {
                    string query =
                        " SELECT CarID " +
                        " FROM Cars " +
                        " WHERE Status = 1 AND TotalQuantity > 0 ";
                    using (var reader = Provider.GetReader(query, CommandType.Text, out connection))
                    {
                        while (reader.Read())
                        {
                            carIDList.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return carIDList;
        }


        public bool AddNewCar(CarObject car)
        {
            bool valid = false;
            try
            {
                string query =
                    " INSERT INTO Cars (CarModel, CarName, Price, LoadCapacity, SeatNum, FuelCapacity, CarType, Manufacturer, Status, TotalQuantity) " +
                    " VALUES (@carModel, @carName, @price, @loadCapacity, @seatNum, @fuelCapacity, @carType, @manufacturer, 1, @totalQuantity) ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@carModel", car.CarModel),
                    new SqlParameter("@carName", car.CarName),
                    new SqlParameter("@price", car.Price),
                    new SqlParameter("@loadCapacity", car.LoadCapacity),
                    new SqlParameter("@seatNum", car.SeatNum),
                    new SqlParameter("@fuelCapacity", car.FuelCapacity),
                    new SqlParameter("@carType", car.CarType),
                    new SqlParameter("@manufacturer", car.Manufacturer),
                    new SqlParameter("@totalQuantity", car.TotalQuantity)
                };
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, paramList);
            } catch (Exception)
            {

            }
            return valid;
        }

        public bool UpdateCar(CarObject car)
        {
            bool valid = false;
            try
            {
                string query =
                    " UPDATE Cars " +
                    " SET CarModel = @carModel, CarName = @carName, Price = @price, LoadCapacity = @loadCapacity, SeatNum = @seatNum, FuelCapacity = @fuelCapacity, CarType = @carType, Manufacturer = @manufacturer, TotalQuantity = @totalQuantity, Status = @status " +
                    " WHERE CarId = @carID ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@carID", car.CarId),
                    new SqlParameter("@carModel", car.CarModel),
                    new SqlParameter("@carName", car.CarName),
                    new SqlParameter("@price", car.Price),
                    new SqlParameter("@loadCapacity", car.LoadCapacity),
                    new SqlParameter("@seatNum", car.SeatNum),
                    new SqlParameter("@fuelCapacity", car.FuelCapacity),
                    new SqlParameter("@carType", car.CarType),
                    new SqlParameter("@manufacturer", car.Manufacturer),
                    new SqlParameter("@totalQuantity", car.TotalQuantity),
                    new SqlParameter("@status", car.Status),
                };
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, paramList);
            }
            catch (Exception)
            {

            }
            return valid;
        }

        public bool DeleteCar(int carID)
        {
            bool valid = false;
            try
            {
                string query =
                    " UPDATE Cars " +
                    " SET Status = 0 " +
                    " WHERE CarId = @carID";
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, new SqlParameter("@carID", carID));
            }
            catch (Exception)
            {

            }
            return valid;
        }

        public bool AddManufacturer(ManufacturerObject manu)
        {
            bool valid = false;
            try
            {
                string query =
                    " INSERT INTO Manufacturers (ManuName, Status) " +
                    " VALUES (@manuName, 1) ";
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, new SqlParameter("@manuName", manu.ManuName));
            } 
            catch (Exception)
            {

            }
            return valid;
        }

        public bool UpdateManufacturer(ManufacturerObject manu)
        {
            bool valid = false;
            try
            {
                string query =
                    " UPDATE Manufacturers " +
                    " SET ManuName = @manuName, Status = @status " +
                    " WHERE ManuID = @manuID ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@manuID", manu.ManuID),
                    new SqlParameter("@manuName", manu.ManuName),
                    new SqlParameter("@status", manu.Status)
                };
                valid = Provider.ExecuteNonQuery(query, CommandType.Text, out connection, paramList);
            }
            catch (Exception)
            {

            }
            return valid;
        }
        public bool DeleteManufacturer(int manuID)
        {
            bool valid = false;
            try
            {
                string queryManu =
                    " UPDATE Manufacturers " +
                    " SET Status = 0 " +
                    " WHERE ManuID = @manuID ; ";
                string queryCar =
                    " Update Cars " +
                    " SET Status = 0 " +
                    " WHERE Manufacturer = @manuID ";
                var paramList = new SqlParameter[]
                {
                    new SqlParameter("@manuID", manuID),
                };
                valid = Provider.ExecuteNonQuery(queryManu + queryCar, CommandType.Text, out connection, paramList);
            }
            catch (Exception)
            {

            }
            return valid;
        }
    }
}
