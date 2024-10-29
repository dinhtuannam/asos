import { ProductNavigate } from '../navigate';
import useBreadcrumb from '@/hooks/useBreadcrumb';

function ProductUpdatePage() {
    useBreadcrumb(ProductNavigate, ProductNavigate.update);
    return <div>ProductUpdatePage</div>;
}

export default ProductUpdatePage;
