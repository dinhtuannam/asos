import MainLayout from '@/layouts/main.layout';
import CartPage from '../pages';
import { CartNavigate } from '../navigate';

export const CartRoutes: Route[] = [
    { path: CartNavigate.cart.link, title: CartNavigate.cart.title, page: CartPage, layout: MainLayout },
];
