import useBreadcrumb from '@/hooks/useBreadcrumb';
import { ProductNavigate } from '../navigate';
//import { User, columns } from '@/components/table/column';
//import { DataTable } from '@/components/table/data-table';

// const users: User[] = [
//     {
//         id: '1',
//         email: 'user1@example.com',
//         phone: '123-456-7890',
//         address: '123 Main St, City, Country',
//     },
//     {
//         id: '2',
//         email: 'user2@example.com',
//         phone: '234-567-8901',
//         address: '456 Elm St, City, Country',
//     },
//     {
//         id: '3',
//         email: 'user3@example.com',
//         phone: '345-678-9012',
//         address: '789 Oak St, City, Country',
//     },
//     {
//         id: '4',
//         email: 'user4@example.com',
//         phone: '456-789-0123',
//         address: '101 Pine St, City, Country',
//     },
//     {
//         id: '5',
//         email: 'user5@example.com',
//         phone: '567-890-1234',
//         address: '202 Maple St, City, Country',
//     },
//     {
//         id: '6',
//         email: 'user6@example.com',
//         phone: '678-901-2345',
//         address: '303 Birch St, City, Country',
//     },
//     {
//         id: '7',
//         email: 'user7@example.com',
//         phone: '789-012-3456',
//         address: '404 Cedar St, City, Country',
//     },
//     {
//         id: '8',
//         email: 'user8@example.com',
//         phone: '890-123-4567',
//         address: '505 Willow St, City, Country',
//     },
//     {
//         id: '9',
//         email: 'user9@example.com',
//         phone: '901-234-5678',
//         address: '606 Fir St, City, Country',
//     },
//     {
//         id: '10',
//         email: 'user10@example.com',
//         phone: '012-345-6789',
//         address: '707 Spruce St, City, Country',
//     },
// ];

export default function ProductPage() {
    useBreadcrumb(ProductNavigate, ProductNavigate.product);
    return <div>{/* <DataTable columns={columns} dataTable={users} searchCol={'address'} /> */}</div>;
}
