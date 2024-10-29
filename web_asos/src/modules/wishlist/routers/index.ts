import MainLayout from '@/layouts/main.layout';
import WishlistPage from '../pages';
import { WishlistNavigate } from '../navigate';

export const WishlistRoutes: Route[] = [
    {
        path: WishlistNavigate.wishlist.link,
        title: WishlistNavigate.wishlist.title,
        page: WishlistPage,
        layout: MainLayout,
    },
];
