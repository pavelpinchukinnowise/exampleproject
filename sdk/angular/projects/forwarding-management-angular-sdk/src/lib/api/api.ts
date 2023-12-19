export * from './locations.service';
import { LocationsService } from './locations.service';
export * from './ports.service';
import { PortsService } from './ports.service';
export * from './quotations.service';
import { QuotationsService } from './quotations.service';
export * from './quotes.service';
import { QuotesService } from './quotes.service';
export const APIS = [LocationsService, PortsService, QuotationsService, QuotesService];
