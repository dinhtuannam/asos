import useBreadcrumb from '@/hooks/useBreadcrumb';
import { ProductNavigate } from '../navigate';

function ProductDetailPage() {
    useBreadcrumb(ProductNavigate, ProductNavigate.detail);

    return <div>ProductDetailPage</div>;
}

export default ProductDetailPage;
