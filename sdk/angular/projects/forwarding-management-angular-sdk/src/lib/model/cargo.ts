/**
 * Forwarding Management API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1.0
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { BulkItem } from './bulkItem';
import { CargoSpecifications } from './cargoSpecifications';
import { CargoType } from './cargoType';
import { Container } from './container';
import { LooseGoodsItem } from './looseGoodsItem';

export interface Cargo { 
    type?: CargoType;
    containers?: Array<Container>;
    bulkItems?: Array<BulkItem>;
    looseGoodsItems?: Array<LooseGoodsItem>;
    specifications?: CargoSpecifications;
}