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
import { PortType } from './portType';

export interface Port { 
    id?: number;
    latitude?: number;
    longitude?: number;
    postalCode?: string;
    address?: string;
    country?: string;
    countryCode?: string;
    internationalCode?: string;
    name?: string;
    type?: PortType;
}