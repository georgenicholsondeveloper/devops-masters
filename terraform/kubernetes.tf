resource "azurerm_kubernetes_cluster" "aks" {
  name                = "aks-commodity-api"
  location            = azurerm_resource_group.rg_k8s.location
  resource_group_name = azurerm_resource_group.rg_k8s.name
  dns_prefix          = "commodity-api"
  
  oidc_issuer_enabled       = true
  workload_identity_enabled = true

  default_node_pool {
    name       = "system"
    node_count = 1
    vm_size    = "Standard_B4als_v2"
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
