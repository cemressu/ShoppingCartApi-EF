using Npgsql;
using PostrgreSqlApi.Model;


namespace ShoppingCartApi
{
    public class DbContext
    {
        private readonly string _connectionString;  //veritabanına bağlanmak için bağlantı dizesi burda saklıyo

        public DbContext(string connectionString) // metod tanımlar bağlantı dizesini alır DbContext sınıfının örneğini oluşturur
        {
            _connectionString = connectionString;
        }



        public List<CustomerModel> GetAllCustomers() //veritabanından tüm müşteri kayıtlarını çeker
        {
            List<CustomerModel> customers = new List<CustomerModel>(); //çektiği müşteri kayıtlarını listeler

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString)) //burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantıyı açıyor

                string sql = "select id, name, surname, phone_number, address, email from customer";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) // burda da customer tablosundaki bağlantıyı sağlıyor
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader()) //sorgudan dönen sonuçlar okunuyor
                    {
                        while (reader.Read()) //burda while döngüsüne sokuyoruz her bir müşteri kaydını okudukça listeye ekliyor
                        {
                            CustomerModel customer = new CustomerModel   //burada yeni bir customermodel nesnesi üretip aşağıdaki satırlarda kolonları ve içlerindeki verileri okuyup listeye ekliyor
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Surname = reader["surname"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Address = reader["address"].ToString(),
                                Email = reader["email"].ToString()
                            };
                            customers.Add(customer);
                        }
                    }
                }
                connection.Close();
            }
            return customers;

        }




        public CustomerModel GetCustomerById(int id) //belirtilen id ye sahip customer bilgilerini alır
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString)) //veritabanımla bağlantıyı kuruyor
            {
                connection.Open(); //bağlantıyı açıyor

                string sql = "select id, name, surname, phone_number, address, email from customer where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) // sql komutuyla tabloyla bağlantı kuruyor
                {
                    cmd.Parameters.AddWithValue("id", id); //parametre olarak belirttiğimiz id yi alıyor

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) //eğer aşağıdaki kolonları ve verileri alırsan o customeri döndür
                        {
                            CustomerModel customer = new CustomerModel
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Surname = reader["surname"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Address = reader["address"].ToString(),
                                Email = reader["email"].ToString()
                            };
                            return customer;
                        }
                        else //aksi takdirde bağlantıyı kapat ve null döndür diyor 
                        {
                            connection.Close ();
                            return null;
                        }
                    }
                }
            }
        }



        public void AddCustomer(CustomerModel customer)  //müşteri ekleme metodu 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString)) //bağlantıyı kuruyor veritabanımla
            {
                connection.Open();  //bağlantıyı açıyor

                string sql = "insert into customer (id, name, surname, phone_number, address, email) values (@id, @name, @surname, @phone_number, @address, @email)"; //insert into komutuyla tabloya yeni kayıt ekliyor
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri ekliyor ('@name','@surname') ve cmd.ExecuteNonQuery ile sorgu başlatılıyor
                {
                    cmd.Parameters.AddWithValue("id", customer.ID);
                    cmd.Parameters.AddWithValue("name", customer.Name);
                    cmd.Parameters.AddWithValue("surname", customer.Surname);
                    cmd.Parameters.AddWithValue("phone_number", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("address", customer.Address);
                    cmd.Parameters.AddWithValue("email", customer.Email);

                    cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için 
                }
                connection.Close();
            }
        }



        public string UpdateCustomer(int id, CustomerModel customer) //müşteri bilgilerini güncelleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString)) //veritabanımla bağlantıyı kuruyor
            {
                connection.Open(); //bağlantıyı açıyor
                int rowsAffected = id;

                string sql = "update customer set id = @id, name = @name, surname = @surname, phone_number = @phone_number, address = @address, email = @email where id = @id"; //update komutuyla kayıt güncelliyor
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri alıp sorguyu başlatıyor
                {
                    cmd.Parameters.AddWithValue("id", customer.ID);
                    cmd.Parameters.AddWithValue("name", customer.Name);
                    cmd.Parameters.AddWithValue("surname", customer.Surname);
                    cmd.Parameters.AddWithValue("phone_number", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("address", customer.Address);
                    cmd.Parameters.AddWithValue("email", customer.Email);

                    rowsAffected = cmd.ExecuteNonQuery(); //güncelleme başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)  //satır kontrol ediyor hiç değişiklik yapılıp yapılmadığına bakıyor
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
                
            }
        }



        public string DeleteCustomer(int id) //belirli müşteriyi silme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString)) //veritabanımla bağlantıyı kurdu
            {
                connection.Open(); //bağlantıyı açtı
                int rowsAffected = id;

                string sql = "delete from customer where id = @id"; //delete komutuyla bir müşteri kaydını sildi
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);//silinecek sepetin id si eklenir ve sorgu başlar


                    rowsAffected = cmd.ExecuteNonQuery(); //sql komutu çalıştıkça silinen satır sayısını tutar
                                                       //silme işlemi başarılıysa true, başarısızsa false döner
                  

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için

                    }

                    if(rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString(); //rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }



        //-------------------------CUSTOMER BİTTİ!-------------------------



        public List<BasketModel> GetAllBaskets()//veritabanından tüm sepet kayıtlarını çeker
        {
            List<BasketModel> baskets = new List<BasketModel>();//çektiği sepet kayıtlarını listeler

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor


                string sql = "select id, customer_id, product_id, quantity, total_price, added_date, status from basket";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader()) //sorgudan dönen sonuçlar okunuyor
                    {

                        while (reader.Read())//burda while döngüsüne sokuyoruz her bir sepet kaydını okudukça listeye ekliyor
                        {

                            BasketModel basket = new BasketModel
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                CustomerID = Convert.ToInt32(reader["customer_id"]),
                                ProductID = Convert.ToInt32(reader["product_id"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                TotalPrice = Convert.ToDecimal(reader["total_price"]),
                                AddedDate = Convert.ToDateTime(reader["added_date"]),
                                Status = reader["status"].ToString()
                            };
                            baskets.Add(basket);
                        }
                    }
                }
                connection.Close();
            }
            return baskets;
        }



        public BasketModel GetBasketById(int id)//belirtilen id ye sahip basket bilgilerini alır
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "select id, customer_id, product_id, quantity, total_price, added_date, status from basket where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);//parametre olarak belirttiğimiz id yi alıyor

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())//eğer aşağıdaki kolonları ve verileri alırsan o basketi döndür
                        {
                            BasketModel basket = new BasketModel
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                CustomerID = Convert.ToInt32(reader["customer_id"]),
                                ProductID = Convert.ToInt32(reader["product_id"]),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                TotalPrice = Convert.ToDecimal(reader["total_price"]),
                                AddedDate = Convert.ToDateTime(reader["added_date"]),
                                Status = reader["status"].ToString()
                            };
                            return basket;
                        }
                        else //aksi takdirde bağlantıyı kapat ve null döndür diyor 
                        {
                            connection.Close();
                            return null;
                        }
                    }
                }
            }
        }



        public void AddBasket(BasketModel basket) //yeni sipariş ekleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "insert into basket (id, customer_id, product_id, quantity, total_price, added_date, status) values (@id, @customer_id, @product_id, @quantity, @total_price, @added_date, @status)";//insert into komutuyla tabloya yeni sipariş ekliyor
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri ekliyor ('@id','@customer_id') ve cmd.ExecuteNonQuery ile sorgu başlatılıyor
                {
                    cmd.Parameters.AddWithValue("id", basket.ID);
                    cmd.Parameters.AddWithValue("customer_id", basket.CustomerID);
                    cmd.Parameters.AddWithValue("product_id", basket.ProductID);
                    cmd.Parameters.AddWithValue("quantity", basket.Quantity);
                    cmd.Parameters.AddWithValue("total_price", basket.TotalPrice);
                    cmd.Parameters.AddWithValue("added_date", basket.AddedDate);
                    cmd.Parameters.AddWithValue("status", basket.Status);

                    cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                }
                connection.Close();
            }
        }



        public string UpdateBasket(int id, BasketModel basket) //sepeti güncelleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor
                int rowsAffected = id;

                string sql = "update basket set id = @id, customer_id = @customer_id, product_id = @product_id, quantity = @quantity, total_price = @total_price, added_date = @added_date, status = @status where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))//parametreleri alıp sorguyu başlatıyor
                {
                    cmd.Parameters.AddWithValue("id", basket.ID);
                    cmd.Parameters.AddWithValue("customer_id", basket.CustomerID);
                    cmd.Parameters.AddWithValue("product_id", basket.ProductID);
                    cmd.Parameters.AddWithValue("quantity", basket.Quantity);
                    cmd.Parameters.AddWithValue("total_price", basket.TotalPrice);
                    cmd.Parameters.AddWithValue("added_date", basket.AddedDate);
                    cmd.Parameters.AddWithValue("status", basket.Status);

                    rowsAffected = cmd.ExecuteNonQuery(); //güncelleme başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }


                        connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }



        public string DeleteBasket(int id) //belirtilen id ye sahip sepeti silme metodu
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantıyı açıyor
                int rowsAffected = id;

                string sql = "delete from basket where id = @id";//delete sql komutuyla bir siparişi sildi
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {

                    cmd.Parameters.AddWithValue("id", id);//silinecek sepetin id si eklenir ve sorgu başlar


                    rowsAffected = cmd.ExecuteNonQuery(); //sql komutu çalıştıkça silinen satır sayısını tutar
                                                          //silme işlemi başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }



        //-------------------------BASKET BİTTİ!-------------------------



        public List<OrdersModel> GetAllOrders()//veritabanından tüm sipariş kayıtlarını çeker
        {
            List<OrdersModel> orders = new List<OrdersModel>();//çektiği sipariş kayıtlarını listeler

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "select id, customer_id, product_id, order_date, total_price, status, cargo_company, quantity from orders";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader()) //sorgudan dönen sonuçlar okunuyor
                    {
                        while (reader.Read())//burda while döngüsüne sokuyoruz her bir sipariş kaydını okudukça listeye ekliyor
                        {
                            OrdersModel order = new OrdersModel
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                CustomerID = Convert.ToInt32(reader["customer_id"]),
                                ProductID = Convert.ToInt32(reader["product_id"]),
                                OrderDate = Convert.ToDateTime(reader["order_date"]),
                                TotalPrice = Convert.ToDecimal(reader["total_price"]),
                                Status = reader["status"].ToString(),
                                CargoCompany = reader["cargo_company"].ToString(),
                                Quantity = Convert.ToInt32(reader["quantity"])


                            };
                            orders.Add(order);

                        }
                    }
                }
                connection.Close();
            }
            return orders;
        }



        public OrdersModel GetOrderById(int id)//belirtilen id ye sahip sipariş bilgilerini alır
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "select id, customer_id, product_id, order_date, total_price, status, cargo_company, quantity from orders where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);//parametre olarak belirttiğimiz id yi alıyor

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            OrdersModel order = new OrdersModel
                            {
                                
                                ID = Convert.ToInt32(reader["id"]),
                                CustomerID = Convert.ToInt32(reader["customer_id"]),
                                ProductID = Convert.ToInt32(reader["product_id"]),
                                OrderDate = Convert.ToDateTime(reader["order_date"]),
                                TotalPrice = Convert.ToDecimal(reader["total_price"]),
                                Status = reader["status"].ToString(),
                                CargoCompany = reader["cargo_company"].ToString(),
                                Quantity = Convert.ToInt32(reader["quantity"])


                            };
                            return order;
                        }
                        else
                        {
                            connection.Close() ;
                            return null;
                        }
                    }
                }
            }
        }



        public void AddOrder(OrdersModel order) //yeni sipariş ekleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "insert into orders (id, customer_id, product_id, order_date, total_price, status, cargo_company, quantity) values (@id, @customer_id, @product_id, @order_date, @total_price, @status, @cargo_company, @quantity)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri ekliyor
                {
                    cmd.Parameters.AddWithValue("id", order.ID);
                    cmd.Parameters.AddWithValue("customer_id", order.CustomerID);
                    cmd.Parameters.AddWithValue("product_id", order.ProductID);
                    cmd.Parameters.AddWithValue("order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("total_price", order.TotalPrice);
                    cmd.Parameters.AddWithValue("status", order.Status);
                    cmd.Parameters.AddWithValue("cargo_company", order.CargoCompany);
                    cmd.Parameters.AddWithValue("quantity", order.Quantity);


                    cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                }
                connection.Close();
            }
        }



        public string UpdateOrder(int id, OrdersModel order) //siparişi güncelleme metodu
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();
                int rowsAffected = id;

                string sql = "update orders set id = @id, customer_id = @customer_id, product_id = @product_id, order_date = @order_date, total_price = @total_price, status = @status, cargo_company = @cargo_company, quantity = @quantity where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))//parametreleri alıp sorguyu başlatıyor
                {
                    
                    cmd.Parameters.AddWithValue("id", order.ID);
                    cmd.Parameters.AddWithValue("customer_id", order.CustomerID);
                    cmd.Parameters.AddWithValue("product_id", order.ProductID);
                    cmd.Parameters.AddWithValue("order_date", order.OrderDate);
                    cmd.Parameters.AddWithValue("total_price", order.TotalPrice);
                    cmd.Parameters.AddWithValue("status", order.Status);
                    cmd.Parameters.AddWithValue("cargo_company", order.CargoCompany);
                    cmd.Parameters.AddWithValue("quantity", order.Quantity);


                    rowsAffected = cmd.ExecuteNonQuery(); //güncelleme başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
                
            }
        }



        public string DeleteOrder(int id) //id si belirtilen siparişi silme metodu
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();
                int rowsAffected = id;

                string sql = "delete from orders where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //usingin içinde o id deki satırın silinip silinmediğine bu karar veriyor
                {

                    cmd.Parameters.AddWithValue("id", id);//silinecek siparişin id si eklenir ve sorgu başlar


                    rowsAffected = cmd.ExecuteNonQuery(); //sql komutu çalıştıkça silinen satır sayısını tutar
                                                          //silme işlemi başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }



        //-------------------------ORDERS BİTTİ!-------------------------



        public List<ProductModel> GetAllProducts()//veritabanından tüm ürün kayıtlarını çeker
        {
            List<ProductModel> products = new List<ProductModel>(); //çektiği ürün kayıtlarını listeler

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantı açıldı

                string sql = "select id, name, price, stock, category, brand from product";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader()) //sorgudan dönen sonuçlar okunuyor
                    {
                        while (reader.Read())//burda while döngüsüne sokuyoruz her bir ürün kaydını okudukça listeye ekliyor
                        {
                            ProductModel product = new ProductModel()
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                Category = reader["category"].ToString(),
                                Brand = reader["brand"].ToString(),
                            };
                            products.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return products;
        }



        public ProductModel GetProductById(int id)//belirtilen id ye sahip ürün bilgilerini alır
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantıyı açıyor 

                string sql = "select id, name, price, stock, category, brand from product where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("id", id);//parametre olarak belirttiğimiz id yi alıyor

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ProductModel product = new ProductModel()
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Name = reader["name"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                Stock = Convert.ToInt32(reader["stock"]),
                                Category = reader["category"].ToString(),
                                Brand = reader["brand"].ToString(),
                            };
                            return product;
                        }
                        else
                        {
                            connection.Close();
                            return null;
                        }
                    }
                }
            }
        }



        public void AddProduct(ProductModel product) //yeni ürün ekleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open();//bağlantıyı açıyor

                string sql = "insert into product (id, name, price, stock, category, brand) values (@id, @name, @price, @stock, @category, @brand)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri ekliyor
                {
                    cmd.Parameters.AddWithValue("id", product.ID);
                    cmd.Parameters.AddWithValue("name", product.Name);
                    cmd.Parameters.AddWithValue("price", product.Price);
                    cmd.Parameters.AddWithValue("stock", product.Stock);
                    cmd.Parameters.AddWithValue("category", product.Category);
                    cmd.Parameters.AddWithValue("brand", product.Brand);

                    cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                }
                connection.Close();
            }
        }



        public string UpdateProduct(int id, ProductModel product) //ürün bilgilerini güncelleme metodu
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantıyı kuruyor
                int rowsAffected = id;


                string sql = "update product set id = @id, name = @name, price = @price, stock = @stock, category = @category, brand = @brand where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection)) //parametreleri alıyor ve sorguyu başlatıyor
                {
                    cmd.Parameters.AddWithValue("id", product.ID);
                    cmd.Parameters.AddWithValue("name", product.Name);
                    cmd.Parameters.AddWithValue("price", product.Price);
                    cmd.Parameters.AddWithValue("stock", product.Stock);
                    cmd.Parameters.AddWithValue("category", product.Category);
                    cmd.Parameters.AddWithValue("brand", product.Brand);


                    rowsAffected = cmd.ExecuteNonQuery(); //güncelleme başarılıysa true, başarısızsa false döner

                    if (rowsAffected > 0)
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }



        public string DeleteProduct(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))//burası veritabanına bağlantıyı sağlamak için
            {
                connection.Open(); //bağlantıyı açıyor
                int rowsAffected = id;

                string sql = "delete from product where id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, connection))//delete komutuyla ürünü veritabından siliyor
                {
                    cmd.Parameters.AddWithValue("id", id);//silinecek ürünün id si eklenir ve sorgu başlar


                    rowsAffected = cmd.ExecuteNonQuery(); //sql komutu çalıştıkça silinen satır sayısını tutar
                                                          //silme işlemi başarılıysa true, başarısızsa false döner
                    if (rowsAffected > 0)//silinen satır sayısı 0dan büyükse komutları çalıştır diyor
                    {
                        cmd.ExecuteNonQuery();//bu ifade sql komutlarını çalıştırmak için
                    }

                    if (rowsAffected == null || rowsAffected == 0)
                    {
                        return rowsAffected.ToString();
                    }

                    connection.Close();
                    return rowsAffected.ToString();//rowsaffected değişkeninin degerini metin olarak yazdırmak için
                }
            }
        }

    }
}



//-------------------------PRODUCT BİTTİ!-------------------------







    

