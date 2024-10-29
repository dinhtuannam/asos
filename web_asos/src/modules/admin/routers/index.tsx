import AuthLayout from '@/modules/auth/layout/auth.layout';
import AdminLayout from '../layout/index';
import { AdminNavigate } from '../navigate';
import {
    AdminLogin,
    BrandManagement,
    CategoryManagement,
    Dashboard,
    DiscountManagement,
    OrderManagement,
    ProductManagement,
    SizeManagement,
    UserManagement,
    ColorManagement,
} from '../pages';
import AdminMiddleware from '@/middlewares/admin.middleware';

export const AdminRoutes: Route[] = [
    {
        path: AdminNavigate.login.link,
        title: AdminNavigate.login.title,
        page: AdminLogin,
        layout: AuthLayout,
    },
    {
        path: AdminNavigate.product.link,
        title: AdminNavigate.product.title,
        page: ProductManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.category.link,
        title: AdminNavigate.category.title,
        page: CategoryManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.brand.link,
        title: AdminNavigate.brand.title,
        page: BrandManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.size.link,
        title: AdminNavigate.size.title,
        page: SizeManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.color.link,
        title: AdminNavigate.color.title,
        page: ColorManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.order.link,
        title: AdminNavigate.order.title,
        page: OrderManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.discount.link,
        title: AdminNavigate.discount.title,
        page: DiscountManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.user.link,
        title: AdminNavigate.user.title,
        page: UserManagement,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
    {
        path: AdminNavigate.dashboard.link,
        title: AdminNavigate.dashboard.title,
        page: Dashboard,
        layout: AdminLayout,
        middleware: AdminMiddleware,
    },
];
