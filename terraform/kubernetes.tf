resource "azurerm_kubernetes_cluster" "aks" {
  name                = "aks-commodity-api"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  dns_prefix          = "commodity-api"

  default_node_pool {
    name       = "system"
    node_count = 1
    vm_size    = "Standard_B2s"
  }

  identity {
    type = "SystemAssigned"
  }
}

locals {
  namespaces = ["dev", "uat", "prod"]
}

resource "kubernetes_namespace_v1" "env" {
  for_each = toset(local.namespaces)

  metadata {
    name = each.value
  }
}
