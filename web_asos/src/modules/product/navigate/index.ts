export const ProductNavigate: NavigateType = {
    product: {
        title: 'Sản phẩm',
        link: '/product',
    },
    detail: {
        title: 'Chi tiết sản phẩm',
        link: '/product/detail',
        parent: 'product',
    },
    update: {
        title: 'Cập nhật sản phẩm',
        link: '/product/detail/update',
        parent: 'detail',
    },
};
