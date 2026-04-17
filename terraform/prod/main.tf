provider "azurerm" {
  features {}
}

terraform {
  required_version = ">= 1.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.0"
    }
  }
}

resource "azurerm_resource_group" "rg" {
  name     = "rg-p-commodity-api"
  location = "francecentral"

  tags = {
    Environment = "prod"
    Project     = "Commodity API"
  }
}