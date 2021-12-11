import { Iproduct } from './product';

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    totalItems: number;
    data: Iproduct[];
}