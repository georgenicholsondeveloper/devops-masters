resource "random_string" "suffix" {
  length  = 6
  special = false
  upper   = false
}

resource "azurerm_mssql_server" "sql" {
  name                         = "commodity-sql-uat-${random_string.suffix.result}"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"

  administrator_login          = "sqladmin"
  administrator_login_password = var.sql_admin_password

  minimum_tls_version           = "1.2"
  public_network_access_enabled = true
}

resource "azurerm_mssql_database" "db" {
  name      = "commoditydb"
  server_id = azurerm_mssql_server.sql.id

  sku_name    = "Basic"
  max_size_gb = 2
}

resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name      = "AllowAzureServices"
  server_id = azurerm_mssql_server.sql.id

  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

output "sql_connection_string" {
  sensitive = true
  value = "Server=tcp:${azurerm_mssql_server.sql.fully_qualified_domain_name},1433;Database=${azurerm_mssql_database.db.name};User ID=${azurerm_mssql_server.sql.administrator_login};Password=${var.sql_admin_password};Encrypt=True;TrustServerCertificate=False;"
}
