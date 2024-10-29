import { useState, useEffect } from 'react';
import { Eye, History, ChevronDown } from 'lucide-react';
import { Button } from '@/components/ui/button';
import { Card, CardContent, CardFooter } from '@/components/ui/card';
import { Badge } from '@/components/ui/badge';
import useModalContext from '@/hooks/useModal';
import { ModalType } from '@/enums/modal.enum';
import MyOrderSkeleton from '../components/skeletons/my-order.skeleton';

interface Order {
    id: string;
    subTotal: number;
    discount: { code: string; amount: number };
    pointsUsed: number;
    dateOrdered: string;
    lastUpdated: string;
    status: 'Pending' | 'Placed' | 'Packed' | 'Shipping' | 'Completed' | 'Refunded';
}

const statusColors = {
    Pending: 'bg-orange-500',
    Placed: 'bg-blue-500',
    Packed: 'bg-cyan-500',
    Shipping: 'bg-purple-500',
    Completed: 'bg-green-500',
    Refunded: 'bg-red-500',
};

const statusTranslations = {
    Pending: 'Pending',
    Placed: 'Placed',
    Packed: 'Packed',
    Shipping: 'Shipping',
    Completed: 'Completed',
    Refunded: 'Refunded',
};

// Fake data
const fakeOrders: Order[] = [
    {
        id: 'ORD-001',
        subTotal: 150000,
        discount: { code: 'SUMMER10', amount: 15000 },
        pointsUsed: 100,
        dateOrdered: '2023-06-01',
        lastUpdated: '2023-06-02',
        status: 'Completed',
    },
    {
        id: 'ORD-002',
        subTotal: 200000,
        discount: { code: 'NEWUSER', amount: 20000 },
        pointsUsed: 0,
        dateOrdered: '2023-06-05',
        lastUpdated: '2023-06-06',
        status: 'Shipping',
    },
    {
        id: 'ORD-003',
        subTotal: 300000,
        discount: { code: '', amount: 0 },
        pointsUsed: 200,
        dateOrdered: '2023-06-10',
        lastUpdated: '2023-06-10',
        status: 'Pending',
    },
    // Add more fake orders here...
];

function MyOrderPage() {
    const [orders, setOrders] = useState<Order[]>([]);
    const [visibleOrders, setVisibleOrders] = useState(5);
    const [isLoading, setIsLoading] = useState(true);
    const { openModal } = useModalContext();

    useEffect(() => {
        // Simulating API call
        setIsLoading(true);
        setTimeout(() => {
            setOrders(fakeOrders);
            setIsLoading(false);
        }, 1500);
    }, []);

    const showMoreOrders = () => {
        setVisibleOrders((prevVisible) => prevVisible + 5);
    };

    const showOrderDetail = (order: Order) => {
        openModal(ModalType.DetailOrder, { email: 'test@example.com', username: 'user123', order });
    };

    const showOrderHistory = (order: Order) => {
        openModal(ModalType.OrderHistory, { email: 'test@example.com', username: 'user123', order });
    };

    return (
        <div className="container mx-auto p-4">
            <h1 className="text-2xl font-bold mb-6">Order History</h1>
            <div className="space-y-4">
                {isLoading ? (
                    <MyOrderSkeleton />
                ) : (
                    orders.slice(0, visibleOrders).map((order) => (
                        <Card key={order.id}>
                            <CardContent className="p-6">
                                <div className="flex justify-between items-start">
                                    <div className="space-y-2">
                                        <p className="font-semibold">Order ID: {order.id}</p>
                                        <p>Total: ${order.subTotal.toLocaleString()}</p>
                                        <p>
                                            Discount: ${order.discount.amount.toLocaleString()} (Code:{' '}
                                            {order.discount.code || 'None'})
                                        </p>
                                        <p>Points used: {order.pointsUsed}</p>
                                        <p>Order date: {order.dateOrdered}</p>
                                        <p>Last updated: {order.lastUpdated}</p>
                                    </div>
                                    <Badge className={`${statusColors[order.status]} text-white`}>
                                        {statusTranslations[order.status]}
                                    </Badge>
                                </div>
                            </CardContent>
                            <CardFooter className="flex justify-end space-x-2">
                                <Button variant="outline" size="sm" onClick={() => showOrderDetail(order)}>
                                    <Eye className="mr-2 h-4 w-4" />
                                    View details
                                </Button>
                                <Button variant="outline" size="sm" onClick={() => showOrderHistory(order)}>
                                    <History className="mr-2 h-4 w-4" />
                                    Order history
                                </Button>
                            </CardFooter>
                        </Card>
                    ))
                )}
            </div>
            {!isLoading && orders.length > visibleOrders && (
                <div className="mt-4 text-center">
                    <Button variant="outline" onClick={showMoreOrders}>
                        Show more
                        <ChevronDown className="ml-2 h-4 w-4" />
                    </Button>
                </div>
            )}
        </div>
    );
}

export default MyOrderPage;
