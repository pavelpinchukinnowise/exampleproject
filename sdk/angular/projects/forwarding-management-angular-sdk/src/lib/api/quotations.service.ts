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
 *//* tslint:disable:no-unused-variable member-ordering */

import { Inject, Injectable, Optional }                      from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams,
         HttpResponse, HttpEvent }                           from '@angular/common/http';
import { CustomHttpUrlEncodingCodec }                        from '../encoder';

import { Observable }                                        from 'rxjs';

import { CargoType } from '../model/cargoType';
import { ErrorResponse } from '../model/errorResponse';
import { InternalErrorResponse } from '../model/internalErrorResponse';
import { QuotationRequestItem } from '../model/quotationRequestItem';
import { QuotationRequestStatus } from '../model/quotationRequestStatus';
import { RequestQuotationCommand } from '../model/requestQuotationCommand';
import { SortByProperty } from '../model/sortByProperty';
import { SortDirection1 } from '../model/sortDirection1';
import { TransportationMode } from '../model/transportationMode';

import { BASE_PATH, COLLECTION_FORMATS }                     from '../variables';
import { Configuration }                                     from '../configuration';


@Injectable()
export class QuotationsService {

    protected basePath = 'http://localhost:5105';
    public defaultHeaders = new HttpHeaders();
    public configuration = new Configuration();

    constructor(protected httpClient: HttpClient, @Optional()@Inject(BASE_PATH) basePath: string, @Optional() configuration: Configuration) {
        if (basePath) {
            this.basePath = basePath;
        }
        if (configuration) {
            this.configuration = configuration;
            this.basePath = basePath || configuration.basePath || this.basePath;
        }
    }

    /**
     * @param consumes string[] mime-types
     * @return true: consumes contains 'multipart/form-data', false: otherwise
     */
    private canConsumeForm(consumes: string[]): boolean {
        const form = 'multipart/form-data';
        for (const consume of consumes) {
            if (form === consume) {
                return true;
            }
        }
        return false;
    }


    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public deleteQuotationRequestByIdEndpoint(id: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public deleteQuotationRequestByIdEndpoint(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public deleteQuotationRequestByIdEndpoint(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public deleteQuotationRequestByIdEndpoint(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling deleteQuotationRequestByIdEndpoint.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/problem+json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('delete',`${this.basePath}/api/quotations/requests/${encodeURIComponent(String(id))}`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param filterString 
     * @param filterTypesOfCargo 
     * @param typesOfTransportationMode 
     * @param filterCreationDateFrom 
     * @param filterCreationDateTo 
     * @param filterTotalWeightMin 
     * @param filterTotalWeightMax 
     * @param filterTotalDimensionsMin 
     * @param filterTotalDimensionsMax 
     * @param filterStatuses 
     * @param filterPriorityShipment 
     * @param pageContinuationToken 
     * @param pageMaxItems 
     * @param sortByProperty 
     * @param sortDirection 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public listQuotationRequestsEndpoint(filterString?: string, filterTypesOfCargo?: Array<CargoType>, typesOfTransportationMode?: Array<TransportationMode>, filterCreationDateFrom?: Date, filterCreationDateTo?: Date, filterTotalWeightMin?: number, filterTotalWeightMax?: number, filterTotalDimensionsMin?: number, filterTotalDimensionsMax?: number, filterStatuses?: Array<QuotationRequestStatus>, filterPriorityShipment?: boolean, pageContinuationToken?: string, pageMaxItems?: number, sortByProperty?: SortByProperty, sortDirection?: SortDirection1, observe?: 'body', reportProgress?: boolean): Observable<Array<QuotationRequestItem>>;
    public listQuotationRequestsEndpoint(filterString?: string, filterTypesOfCargo?: Array<CargoType>, typesOfTransportationMode?: Array<TransportationMode>, filterCreationDateFrom?: Date, filterCreationDateTo?: Date, filterTotalWeightMin?: number, filterTotalWeightMax?: number, filterTotalDimensionsMin?: number, filterTotalDimensionsMax?: number, filterStatuses?: Array<QuotationRequestStatus>, filterPriorityShipment?: boolean, pageContinuationToken?: string, pageMaxItems?: number, sortByProperty?: SortByProperty, sortDirection?: SortDirection1, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<Array<QuotationRequestItem>>>;
    public listQuotationRequestsEndpoint(filterString?: string, filterTypesOfCargo?: Array<CargoType>, typesOfTransportationMode?: Array<TransportationMode>, filterCreationDateFrom?: Date, filterCreationDateTo?: Date, filterTotalWeightMin?: number, filterTotalWeightMax?: number, filterTotalDimensionsMin?: number, filterTotalDimensionsMax?: number, filterStatuses?: Array<QuotationRequestStatus>, filterPriorityShipment?: boolean, pageContinuationToken?: string, pageMaxItems?: number, sortByProperty?: SortByProperty, sortDirection?: SortDirection1, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<Array<QuotationRequestItem>>>;
    public listQuotationRequestsEndpoint(filterString?: string, filterTypesOfCargo?: Array<CargoType>, typesOfTransportationMode?: Array<TransportationMode>, filterCreationDateFrom?: Date, filterCreationDateTo?: Date, filterTotalWeightMin?: number, filterTotalWeightMax?: number, filterTotalDimensionsMin?: number, filterTotalDimensionsMax?: number, filterStatuses?: Array<QuotationRequestStatus>, filterPriorityShipment?: boolean, pageContinuationToken?: string, pageMaxItems?: number, sortByProperty?: SortByProperty, sortDirection?: SortDirection1, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {
















        let queryParameters = new HttpParams({encoder: new CustomHttpUrlEncodingCodec()});
        if (filterString !== undefined && filterString !== null) {
            queryParameters = queryParameters.set('FilterString', <any>filterString);
        }
        if (filterTypesOfCargo) {
            filterTypesOfCargo.forEach((element) => {
                queryParameters = queryParameters.append('FilterTypesOfCargo', <any>element);
            })
        }
        if (typesOfTransportationMode) {
            typesOfTransportationMode.forEach((element) => {
                queryParameters = queryParameters.append('TypesOfTransportationMode', <any>element);
            })
        }
        if (filterCreationDateFrom !== undefined && filterCreationDateFrom !== null) {
            queryParameters = queryParameters.set('FilterCreationDateFrom', <any>filterCreationDateFrom.toISOString());
        }
        if (filterCreationDateTo !== undefined && filterCreationDateTo !== null) {
            queryParameters = queryParameters.set('FilterCreationDateTo', <any>filterCreationDateTo.toISOString());
        }
        if (filterTotalWeightMin !== undefined && filterTotalWeightMin !== null) {
            queryParameters = queryParameters.set('FilterTotalWeightMin', <any>filterTotalWeightMin);
        }
        if (filterTotalWeightMax !== undefined && filterTotalWeightMax !== null) {
            queryParameters = queryParameters.set('FilterTotalWeightMax', <any>filterTotalWeightMax);
        }
        if (filterTotalDimensionsMin !== undefined && filterTotalDimensionsMin !== null) {
            queryParameters = queryParameters.set('FilterTotalDimensionsMin', <any>filterTotalDimensionsMin);
        }
        if (filterTotalDimensionsMax !== undefined && filterTotalDimensionsMax !== null) {
            queryParameters = queryParameters.set('FilterTotalDimensionsMax', <any>filterTotalDimensionsMax);
        }
        if (filterStatuses) {
            filterStatuses.forEach((element) => {
                queryParameters = queryParameters.append('FilterStatuses', <any>element);
            })
        }
        if (filterPriorityShipment !== undefined && filterPriorityShipment !== null) {
            queryParameters = queryParameters.set('FilterPriorityShipment', <any>filterPriorityShipment);
        }
        if (pageContinuationToken !== undefined && pageContinuationToken !== null) {
            queryParameters = queryParameters.set('PageContinuationToken', <any>pageContinuationToken);
        }
        if (pageMaxItems !== undefined && pageMaxItems !== null) {
            queryParameters = queryParameters.set('PageMaxItems', <any>pageMaxItems);
        }
        if (sortByProperty !== undefined && sortByProperty !== null) {
            queryParameters = queryParameters.set('SortByProperty', <any>sortByProperty);
        }
        if (sortDirection !== undefined && sortDirection !== null) {
            queryParameters = queryParameters.set('SortDirection', <any>sortDirection);
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/json',
            'application/problem+json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<Array<QuotationRequestItem>>('get',`${this.basePath}/api/quotations/requests`,
            {
                params: queryParameters,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param body 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public newQuotationRequestEndpoint(body: RequestQuotationCommand, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public newQuotationRequestEndpoint(body: RequestQuotationCommand, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public newQuotationRequestEndpoint(body: RequestQuotationCommand, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public newQuotationRequestEndpoint(body: RequestQuotationCommand, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (body === null || body === undefined) {
            throw new Error('Required parameter body was null or undefined when calling newQuotationRequestEndpoint.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/problem+json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
            'application/json'
        ];
        const httpContentTypeSelected: string | undefined = this.configuration.selectHeaderContentType(consumes);
        if (httpContentTypeSelected != undefined) {
            headers = headers.set('Content-Type', httpContentTypeSelected);
        }

        return this.httpClient.request<any>('post',`${this.basePath}/api/quotations/requests`,
            {
                body: body,
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

    /**
     * 
     * 
     * @param id 
     * @param observe set whether or not to return the data Observable as the body, response or events. defaults to returning the body.
     * @param reportProgress flag to report request and response progress.
     */
    public withdrawQuotationRequestByIdEndpoint(id: string, observe?: 'body', reportProgress?: boolean): Observable<any>;
    public withdrawQuotationRequestByIdEndpoint(id: string, observe?: 'response', reportProgress?: boolean): Observable<HttpResponse<any>>;
    public withdrawQuotationRequestByIdEndpoint(id: string, observe?: 'events', reportProgress?: boolean): Observable<HttpEvent<any>>;
    public withdrawQuotationRequestByIdEndpoint(id: string, observe: any = 'body', reportProgress: boolean = false ): Observable<any> {

        if (id === null || id === undefined) {
            throw new Error('Required parameter id was null or undefined when calling withdrawQuotationRequestByIdEndpoint.');
        }

        let headers = this.defaultHeaders;

        // to determine the Accept header
        let httpHeaderAccepts: string[] = [
            'application/problem+json'
        ];
        const httpHeaderAcceptSelected: string | undefined = this.configuration.selectHeaderAccept(httpHeaderAccepts);
        if (httpHeaderAcceptSelected != undefined) {
            headers = headers.set('Accept', httpHeaderAcceptSelected);
        }

        // to determine the Content-Type header
        const consumes: string[] = [
        ];

        return this.httpClient.request<any>('post',`${this.basePath}/api/quotations/requests/${encodeURIComponent(String(id))}/withdraw`,
            {
                withCredentials: this.configuration.withCredentials,
                headers: headers,
                observe: observe,
                reportProgress: reportProgress
            }
        );
    }

}
