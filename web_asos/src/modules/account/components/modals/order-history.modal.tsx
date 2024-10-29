import React, { useState, useEffect } from 'react';
import DrawerContainer from '@/components/container/drawer.container';
import { ModalType } from '@/enums/modal.enum';
import useModalContext from '@/hooks/useModal';

// Định nghĩa kiểu dữ liệu cho mỗi mục trong lịch sử đơn hàng
interface OrderHistoryItem {
    orderId: string;
    userName: string;
    fromStatus: string;
    toStatus: string;
    timestamp: string;
}

const statusColors = {
    Pending: 'bg-orange-500',
    Placed: 'bg-blue-500',
    Packed: 'bg-cyan-500',
    Shipping: 'bg-purple-500',
    Completed: 'bg-green-500',
    Refunded: 'bg-red-500',
};

const mockData: OrderHistoryItem[] = [
    {
        orderId: 'ORD001',
        userName: 'John Doe',
        fromStatus: 'Pending',
        toStatus: 'Placed',
        timestamp: '2023-04-01 10:30:00',
    },
    {
        orderId: 'ORD002',
        userName: 'Jane Smith',
        fromStatus: 'Placed',
        toStatus: 'Packed',
        timestamp: '2023-04-02 14:45:00',
    },
    {
        orderId: 'ORD003',
        userName: 'Bob Johnson',
        fromStatus: 'Packed',
        toStatus: 'Shipping',
        timestamp: '2023-04-03 09:15:00',
    },
    {
        orderId: 'ORD004',
        userName: 'Alice Brown',
        fromStatus: 'Shipping',
        toStatus: 'Completed',
        timestamp: '2023-04-04 16:20:00',
    },
    {
        orderId: 'ORD005',
        userName: 'Charlie Wilson',
        fromStatus: 'Completed',
        toStatus: 'Refunded',
        timestamp: '2023-04-05 11:00:00',
    },
    {
        orderId: 'ORD006',
        userName: 'Eva Davis',
        fromStatus: 'Pending',
        toStatus: 'Placed',
        timestamp: '2023-04-06 13:30:00',
    },
];

const OrderHistoryModal: React.FC = () => {
    const { modals, closeModal } = useModalContext();
    const OrderHistoryModalData = modals[ModalType.OrderHistory];

    const [historyItems, setHistoryItems] = useState<OrderHistoryItem[]>([]);

    useEffect(() => {
        const data = Array.isArray(OrderHistoryModalData?.data) ? OrderHistoryModalData.data : mockData;
        setHistoryItems(data);
    }, [OrderHistoryModalData?.data]);

    if (!OrderHistoryModalData || !OrderHistoryModalData.visible) return null;

    return (
        <DrawerContainer
            title="Order History"
            open={OrderHistoryModalData.visible}
            onClose={() => closeModal(ModalType.OrderHistory)}
        >
            <div className="px-1 md:w-[400px] sm:w-full">
                <div className="h-[500px] w-full rounded-md border p-4 overflow-y-auto custom-scrollbar">
                    {Array.isArray(historyItems) && historyItems.length > 0 ? (
                        historyItems.map((item, index) => (
                            <div key={index} className="mb-4 rounded-lg border p-4 shadow-sm bg-white">
                                <p className="font-semibold text-black">Order ID: {item.orderId}</p>
                                <p className="text-gray-800 mb-1">User: {item.userName}</p>
                                <p className="text-gray-800 mb-1">
                                    <span className="mr-2">Status:</span>
                                    <span
                                        className={`inline-block px-2 py-1 rounded-full text-white text-xs font-medium mr-2 ${
                                            statusColors[item.fromStatus as keyof typeof statusColors]
                                        }`}
                                    >
                                        {item.fromStatus}
                                    </span>
                                    →
                                    <span
                                        className={`inline-block px-2 py-1 rounded-full text-white text-xs font-medium ml-2 ${
                                            statusColors[item.toStatus as keyof typeof statusColors]
                                        }`}
                                    >
                                        {item.toStatus}
                                    </span>
                                </p>
                                <p className="text-sm text-gray-600">{item.timestamp}</p>
                            </div>
                        ))
                    ) : (
                        <p className="text-gray-800">No order history available.</p>
                    )}
                </div>
            </div>
        </DrawerContainer>
    );
};

export default OrderHistoryModal;
