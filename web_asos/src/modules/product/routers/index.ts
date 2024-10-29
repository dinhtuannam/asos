import { ProductNavigate } from '../navigate';
import MainLayout from '@/layouts/main.layout';
import { ProductPage, ProductDetailPage, ProductUpdatePage } from '../pages';

export const ProductRoutes: Route[] = [
    {
        path: ProductNavigate.product.link,
        title: ProductNavigate.product.title,
        page: ProductPage,
        layout: MainLayout,
    },
    {
        path: ProductNavigate.detail.link,
        title: ProductNavigate.detail.title,
        page: ProductDetailPage,
        layout: MainLayout,
    },
    {
        path: ProductNavigate.update.link,
        title: ProductNavigate.update.title,
        page: ProductUpdatePage,
        layout: MainLayout,
    },
];
