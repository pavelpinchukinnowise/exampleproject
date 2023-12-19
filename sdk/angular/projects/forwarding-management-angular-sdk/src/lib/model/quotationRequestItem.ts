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
import { Cargo } from './cargo';
import { Location } from './location';
import { QuotationRequestStatus } from './quotationRequestStatus';
import { StatusModificationOfQuotationRequestStatus } from './statusModificationOfQuotationRequestStatus';
import { TransportationMode } from './transportationMode';

export interface QuotationRequestItem { 
    id?: string;
    cargo?: Cargo;
    transportationMode?: TransportationMode;
    startingLocation?: Location;
    finalLocation?: Location;
    createdAtTimestamp?: Date;
    status?: QuotationRequestStatus;
    statusModifications?: Array<StatusModificationOfQuotationRequestStatus>;
    isPriorityShipment?: boolean;
}